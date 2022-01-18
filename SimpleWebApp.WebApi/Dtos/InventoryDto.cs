using System.ComponentModel.DataAnnotations;
using SimpleWebApp.WebApi.Enumerations;

namespace SimpleWebApp.WebApi.Dtos;

public sealed class InventoryDto
{
    private string? _itemName;
    private string? _warehouseLocation;

    public long Id { get; set; }
    public string ItemName
    {
        get => _itemName!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _itemName = value;
        }
    }

    public string WarehouseLocation
    {
        get => _warehouseLocation!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _warehouseLocation = value;
        }
    }

    public string? Description { get; set; }
    public GoodType GoodType { get; set; }

    [Required]
    public long ItemId { get; set; }

    [Required]
    public long WarehouseId { get; set; }
    
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "Quantity should be positive integer greater than 0")]
    public long Quantity { get; set; }
}
