using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.DomainService;
using NerdStore.Core.Bus;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependecyInjection
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            
            // Domain BUS (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();


        }
    }
}
