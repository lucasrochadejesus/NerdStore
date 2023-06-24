using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
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
            }

            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if(message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                // show event error

            }

            return false;
        }
    }
}
