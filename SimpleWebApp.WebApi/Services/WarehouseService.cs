using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Extensions;
using SimpleWebApp.WebApi.Repositories;

namespace SimpleWebApp.WebApi.Services;

public sealed class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<IEnumerable<WarehouseDto>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        var warehouses = await _warehouseRepository.GetAsync(cancellationToken);
        return warehouses.Select(x => x.ToDto());
    }

    public Task<long> GetCapacityAsync(long warehouseId,
        CancellationToken cancellationToken = default)
    {
        return _warehouseRepository.GetCapacityAsync(warehouseId, cancellationToken);
    }
}
