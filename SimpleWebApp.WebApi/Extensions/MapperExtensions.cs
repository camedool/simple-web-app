using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Models;

namespace SimpleWebApp.WebApi.Extensions;

internal static class MapperExtensions
{
    internal static Inventory ToModel(this InventoryDto inventoryDto)
    {
        return new Inventory()
        {
            Id = inventoryDto.Id,
            ItemId = inventoryDto.ItemId,
            WarehouseId = inventoryDto.WarehouseId,
            Quantity = inventoryDto.Quantity,
            Description = inventoryDto.Description
        };
    }

    internal static InventoryDto ToDto(this Inventory inventory)
    {
        return new InventoryDto()
        {
            Id = inventory.Id,
            ItemId = inventory.ItemId,
            WarehouseId = inventory.WarehouseId,
            WarehouseLocation = inventory.Warehouse.Location,
            Quantity = inventory.Quantity,
            Description = inventory.Description,
            GoodType = inventory.Item.Type,
            ItemName = inventory.Item.Name
        };
    }

    internal static Item ToModel(this ItemDto itemDto)
    {
        return new Item()
        {
            Id = itemDto.Id,
            Name = itemDto.Name,
            Description = itemDto.Description,
            Type = itemDto.Type
        };
    }

    internal static ItemDto ToDto(this Item item)
    {
        return new ItemDto()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Type = item.Type
        };
    }

    internal static Warehouse ToModel(this WarehouseDto warehouseDto)
    {
        return new Warehouse()
        {
            Id = warehouseDto.Id,
            Name = warehouseDto.Name,
            Location = warehouseDto.Location,
            Capacity = warehouseDto.Capacity
        };
    }

    internal static WarehouseDto ToDto(this Warehouse warehouse)
    {
        return new WarehouseDto(warehouse.Id, warehouse.Name,
            warehouse.Location, warehouse.Capacity);
    }
}
