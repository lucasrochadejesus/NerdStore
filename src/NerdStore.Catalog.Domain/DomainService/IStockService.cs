namespace NerdStore.Catalog.Domain.DomainService;

public interface IStockService : IDisposable
{
    Task<bool> IncreaseStock(Guid productId, int quantity);
    Task<bool> DecreaseStock(Guid productId, int quantity);

}