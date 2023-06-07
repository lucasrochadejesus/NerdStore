using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {

        public Guid CustomerId { get; private set; }

        public Guid ProductId { get; private set; }

        public string ProductName { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public AddOrderItemCommand(Guid customerId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
