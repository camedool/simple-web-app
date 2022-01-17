using SimpleWebApp.WebApi.Dtos;

namespace SimpleWebApp.WebApi.Services;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAsync(
        CancellationToken cancellationToken = default);
    Task<(bool IsCreated, InventoryDto NewInventory)> AddAsync(InventoryDto inventoryDto,
        CancellationToken cancellationToken = default);
    Task<(bool IsUpdated, InventoryDto UpdatedInventory)> UpdateAsync(InventoryDto inventoryDto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(long inventoryId,
        CancellationToken cancellationToken = default);
}
