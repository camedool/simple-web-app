using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public interface IInventoryRepository : IRepository<Inventory>
{
    long GetOccupancy(long warehouseId);
}
