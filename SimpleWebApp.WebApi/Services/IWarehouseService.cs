using SimpleWebApp.WebApi.Dtos;

namespace SimpleWebApp.WebApi.Services;

public interface IWarehouseService
{
    Task<IEnumerable<WarehouseDto>> GetAsync(
        CancellationToken cancellationToken = default);

    Task<long> GetCapacityAsync(long warehouseId,
        CancellationToken cancellationToken = default);
}
