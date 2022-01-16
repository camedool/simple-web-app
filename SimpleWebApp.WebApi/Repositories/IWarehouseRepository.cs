using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public interface IWarehouseRepository : IRepository<Warehouse>
{
    Task<long> GetCapacityAsync(long warehouseId, 
        CancellationToken cancellationToken = default);
}
