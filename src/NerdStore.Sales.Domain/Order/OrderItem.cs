using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain.Order
{
    public class OrderItem : Entity
    {

        public Guid OrderItemId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        //EF Relation
        public Order Order { get; set; }

        public OrderItem(Guid productId,  string productName, int quantity, decimal unitPrice)
        { 
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice; 
        }

        protected OrderItem() { }

        internal void AssociateOrder(Guid orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public decimal Calculate()
        {
            return Quantity * UnitPrice;
        }

        internal void AddUnity(int unity)
        {
            Quantity += unity;
        }

        internal void UpdateUnity(int unity)
        {
            Quantity += unity;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
