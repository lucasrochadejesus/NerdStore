using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Catalog.Domain.DomainService;

public interface IStockService : IDisposable
{
    Task<bool> DecreaseStock(Guid productId, int quantity);

    Task<bool> DecreaseListProductItemStock(ListOrderProducts listOrder);

    Task<bool> IncreaseStock(Guid productId, int quantity);

    Task<bool> IncreaseListProductItemStock(ListOrderProducts listOrder);

}