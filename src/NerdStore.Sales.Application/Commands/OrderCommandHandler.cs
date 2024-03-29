﻿using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateItemOrderCommand, bool>,
        IRequestHandler<RemoveItemOrderCommand, bool>,
        IRequestHandler<ApplyCouponOrderCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {

            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderQuoteByCustomerId(message.CustomerId);

            var orderItem = new OrderItem(message.ProductId, message.ProductName, message.Quantity, message.UnitPrice);

            if (order == null)
            {
                // Create Quote
                order = Order.OrderFactory.NewOrderQuote(message.CustomerId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);
                order.AddEvent(new OrderQuoteStartedEvent(message.CustomerId, message.ProductId));
            }
            else
            {
                var orderItemExists = order.OrderItemExists(orderItem);
                order.AddItem(orderItem);

                if (orderItemExists)
                {
                    _orderRepository
                        .UpdateItem(order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new OrderUpdatedEvent(order.CustomerId,order.Id, order.Total));
            }

            order.AddEvent(new OrderItemAddedEvent(order.CustomerId, order.Id,message.ProductId,message.ProductName, message.UnitPrice, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();

        }

        public async Task<bool> Handle(UpdateItemOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderQuoteByCustomerId(message.CustomerId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not founded!"));
                return false;
            }
             
            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (!order.OrderItemExists(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Item not founded"));
                return false;
            }

            order.UpdateUnity(orderItem, message.Quantity);

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            order.AddEvent(new OrderUpdatedEvent(order.CustomerId, order.Id, order.Total)); 
           
            return await _orderRepository.UnitOfWork.Commit();

        }

        public async Task<bool> Handle(RemoveItemOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderQuoteByCustomerId(message.CustomerId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not founded!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (orderItem != null && !order.OrderItemExists(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Item not founded!"));
                return false;
            }

            order.RemoveItem(orderItem);
            order.AddEvent(new OrderRemoveEvent(message.CustomerId, order.Id, message.ProductId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();

        }

        public async Task<bool> Handle(ApplyCouponOrderCommand message, CancellationToken cancellationToken)
        { 
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderQuoteByCustomerId(message.CustomerId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not founded!"));
                return false;
            }

            var coupon = await _orderRepository.GetCouponByCode(message.CouponCode);
            if (coupon == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Coupon code not founded!"));
                return false;
            }

            var couponApplicableValidation = order.ApplyCoupon(coupon);
            if (!couponApplicableValidation.IsValid)
            {
                foreach (var error in couponApplicableValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new CouponAppliedOrderEvent(message.CustomerId, order.Id, coupon.Id));
            order.AddEvent(new OrderUpdatedEvent(order.CustomerId, order.Id, order.Total));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();

        }

        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderQuoteByCustomerId(message.CustomerId);

            order.MakeDraft();

            var items = new List<Item>();

            order.OrderItems.ForEach(i => items.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));

            var listOrderProducts = new ListOrderProducts { OrderId = order.Id, items = items };

            order.AddEvent(new OrderDraftEvent(order.Id, order.CustomerId, order.Total, listOrderProducts, message.CardName, message.CardNumber, message.ExpirationDate, message.CvvCode));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();

        }

        private bool ValidateCommand(Command message)
        {
            if(message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }


    }
}
