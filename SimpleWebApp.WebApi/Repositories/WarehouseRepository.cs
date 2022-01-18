using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public sealed class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    private readonly AppDbContext _context;
    public WarehouseRepository(AppDbContext context) :
        base(context, context.Warehouses)
    {
        _context = context;
    }

    public Task<long> GetCapacityAsync(long warehouseId, CancellationToken cancellationToken = default)
    {
        return _context.Warehouses
            .Where(x => x.Id == warehouseId)
            .Select(x => x.Capacity)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
