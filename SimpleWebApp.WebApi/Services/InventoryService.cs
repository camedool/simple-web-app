using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Extensions;
using SimpleWebApp.WebApi.Repositories;

namespace SimpleWebApp.WebApi.Services;

public sealed class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<InventoryService> _logger;

    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public InventoryService(
        IInventoryRepository inventoryRepository,
        IWarehouseService warehouseService,
        ILogger<InventoryService> logger)
    {
        _inventoryRepository = inventoryRepository;
        _warehouseService = warehouseService;
        _logger = logger;
    }

    public async Task<(bool IsCreated, InventoryDto NewInventory)> AddAsync(InventoryDto inventoryDto,
        CancellationToken cancellationToken = default)
    {
        var inventory = inventoryDto.ToModel();

        // TODO: consider implementing some sort of cache, for performance reason
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (await CanAddIntoWarehouse(inventory.WarehouseId, inventory.Quantity, cancellationToken))
            {
                await _inventoryRepository.AddAsync(inventory, cancellationToken);
                inventoryDto = inventory.ToDto();
                
                return (true, inventoryDto);
            }

            return (false, inventoryDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't add inventory. Inventory details: {@inventoryDto}", inventoryDto);
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<IEnumerable<InventoryDto>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        var inventories = await _inventoryRepository.GetAsync(cancellationToken);
        return inventories.Select(x => x.ToDto());
    }

    private async Task<bool> CanAddIntoWarehouse(long warehouseId, long newItems,
        CancellationToken cancellationToken = default)
    {
        var currentCapacity = await _warehouseService.GetCapacityAsync(warehouseId, cancellationToken);
        var currentOccupancy = _inventoryRepository.GetOccupancy(warehouseId);

        return currentCapacity >= currentOccupancy + newItems;
    }

    public async Task<(bool IsUpdated, InventoryDto UpdatedInventory)> UpdateAsync(
        InventoryDto inventoryDto, 
        CancellationToken cancellationToken = default)
    {
        var inventory = inventoryDto.ToModel();

        // TODO: consider implementing some sort of cache, for performance reason
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (await CanAddIntoWarehouse(inventory.WarehouseId, inventory.Quantity, cancellationToken))
            {
                await _inventoryRepository.UpdateAsync(inventory, cancellationToken);
                inventoryDto = inventory.ToDto();

                return (true, inventoryDto);
            }

            return (false, inventoryDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't update inventory. Inventory details: {@inventory}", inventoryDto);
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> DeleteAsync(long inventoryId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _inventoryRepository.DeleteAsync(inventoryId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't delete inventory. Inventory id: {@inventoryId}", inventoryId);
            throw;
        }
    }
}
