using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.DomainService;
using NerdStore.Core;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
      
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public ProductAppService(IProductRepository productRepository, IMapper mapper, IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductByCategory(code));
        }

        public async Task<ProductViewModel> GetProductById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
           return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel productViewModel)
        {
           var product = _mapper.Map<Product>(productViewModel);
           
           _productRepository.Add(product);
             
           await _productRepository.UnitOfWork.Commit();

        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();

        }

        public async Task<ProductViewModel> DecreaseStock(Guid id, int amount)
        {
            if (!_stockService.DecreaseStock(id, amount).Result)
            {
                throw new DomainException("Failed to decrease stock");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductById(id));

        }

        public async Task<ProductViewModel> IncreaseStock(Guid id, int amount)
        {
            
            if (!_stockService.IncreaseStock(id, amount).Result)
            {
                throw new DomainException("Failed to increase stock");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductById(id));

        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }

    }
}
