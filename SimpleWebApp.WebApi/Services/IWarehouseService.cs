namespace SimpleWebApp.WebApi.Services;

public interface IWarehouseService
{
    Task<long> GetCapacityAsync(long warehouseId,
        CancellationToken cancellationToken = default);
}
