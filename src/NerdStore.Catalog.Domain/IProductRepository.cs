using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProductById(Guid id); 
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Product>> GetProductByCategory(int code);


        // Product
        void Add(Product product);
        void Update(Product product);


        // Category
        void Add(Category category);
        void Update(Category category);
    }
}
