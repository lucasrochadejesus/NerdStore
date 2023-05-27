using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Catalog.Application.ViewModels;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetProductByCategory(int code);

        Task<ProductViewModel> GetProductById(Guid id);

        Task<IEnumerable<ProductViewModel>> GetAll();

        Task<IEnumerable<CategoryViewModel>> GetCategories();

        Task AddProduct(ProductViewModel productViewModel);

        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseStock(Guid id, int amount);

        Task<ProductViewModel> IncreaseStock(Guid id, int amount);
    }
}
