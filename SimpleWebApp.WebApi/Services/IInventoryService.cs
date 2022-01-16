using SimpleWebApp.WebApi.Dtos;

namespace SimpleWebApp.WebApi.Services;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAsync(
        CancellationToken cancellationToken = default);
    Task<bool> TryAddAsync(InventoryDto inventoryDto,
        CancellationToken cancellationToken = default);
    Task<ItemDto> UpdateAsync(InventoryDto inventoryDto,
        CancellationToken cancellationToken = default);
}
