using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public DateTime DtCreation { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public string Image { get; private set; }
        public string ModelNumber { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}
