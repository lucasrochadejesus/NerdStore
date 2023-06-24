using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repository;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.DomainService;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Data.Repository;
using NerdStore.Sales.Domain.Order;
using IOrderRepository = NerdStore.Sales.Domain.Order.IOrderRepository;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependecyInjection
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            
            // Domain BUS (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<LowStockProductEvent>, ProductEventHandler>();

            // Sales
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();

        }
    }
}
