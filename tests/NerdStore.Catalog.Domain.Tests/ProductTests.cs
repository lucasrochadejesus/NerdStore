using NerdStore.Core;

namespace NerdStore.Catalog.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Validate_ValidationsMustReturnExeception()
        {
            var ex = Assert.Throws<DomainException>(() =>
            {
                new Product(string.Empty, "Description", false, DateTime.Now, 1,Guid.NewGuid(), "Image", "A",0,new Dimensions(1,1,1));
            });

            Assert.Equal("Product cannot be empty", ex.Message);

        }
    }
}