using SimpleWebApp.WebApi.Enumerations;

namespace SimpleWebApp.WebApi.Dtos;

public sealed class InventoryDto
{
    private string? _itemName;

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
    public string? Description { get; set; }
    public GoodType Type { get; set; }
    public long ItemId { get; set; }
    public long WarehouseId { get; set; }
    public long Quantity { get; set; }
}
