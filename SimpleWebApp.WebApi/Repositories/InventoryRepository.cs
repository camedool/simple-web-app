using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Repositories;

public sealed class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    private readonly AppDbContext _context;
    public InventoryRepository(AppDbContext context) :
        base(context, context.Inventories)
    {
        _context = context;
    }

    public long GetOccupancy(long warehouseId)
    {
        return _context.Inventories
            .Where(x => x.WarehouseId == warehouseId)
            .Sum(x => x.WarehouseId);
    }

    public override async Task<IReadOnlyCollection<Inventory>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        var items = await _context.Inventories
            .AsNoTracking()
            .Include(x => x.Item)
            .Include(x => x.Warehouse)
            .ToListAsync(cancellationToken);
        
        return items.AsReadOnly();
    }

    public override async Task<Inventory> AddAsync(Inventory item,
        CancellationToken cancellationToken = default)
    {
        await base.AddAsync(item, cancellationToken);
        IncludeNavProperties(item);

        return item;
    }

    public override async Task<Inventory> UpdateAsync(Inventory item,
    CancellationToken cancellationToken = default)
    {
        await base.UpdateAsync(item, cancellationToken);
        IncludeNavProperties(item);

        return item;
    }

    private void IncludeNavProperties(Inventory item)
    {
        _context.Inventories
            .Include(x => x.Item)
            .Include(x => x.Warehouse)
            .SingleOrDefault(x => x.Id == item.Id);
    }
}
