using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {

        public int BrandId { get; private set; }
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
        public Dimensions Dimensions { get; private set; }
     
        
        protected Product() {}
        public Product(string name, string description, bool active, DateTime dtCreation, decimal price, Guid categoryId, string image, string modelNumber, int brandId, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            DtCreation = dtCreation;
            Price = price;
            Image = image;
            ModelNumber = modelNumber;
            BrandId = brandId;
            Dimensions = dimensions;

            Validate();
         
        }
         
        public void Activate() => Active = true;

        public void Desactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void DecreaseStock(int quantity)
        {
            // module to convert to positive number.
            if (quantity < 0) quantity *= -1;
            StockQuantity -= quantity;
        }

        public void IncreaseStock(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HasStock(int quantity)
        {
            return StockQuantity >= quantity;
        }

        public void Validate()
        {
            AssertionConcern.ValidateEmpty(Name, "Product must have a name");
            AssertionConcern.ValidateEmpty(Description, "Product must have a description");
            AssertionConcern.ValidateIfNotEquals(CategoryId, Guid.Empty, "Product must have Category ID");

        }

         
    }
}
