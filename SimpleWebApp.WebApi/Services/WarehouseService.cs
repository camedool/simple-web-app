using SimpleWebApp.WebApi.Repositories;

namespace SimpleWebApp.WebApi.Services;

public sealed class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public Task<long> GetCapacityAsync(long warehouseId, 
        CancellationToken cancellationToken = default)
    {
        return _warehouseRepository.GetCapacityAsync(warehouseId, cancellationToken);
    }
}
