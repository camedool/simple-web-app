namespace SimpleWebApp.WebApi.Models;

public sealed class Inventory : DbItem
{
    private Item? _item;
    private Warehouse? _warehouse;

    public long ItemId { get; set; }
    public long WarehouseId { get; set; }
    public long Quantity { get; set; }
    public string? Description { get; set; }

    /*
     * Navigation properties
     */
    public Item Item
    {
        get => _item!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _item = value;
        }
    }

    public Warehouse Warehouse
    {
        get => _warehouse!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _warehouse = value;
        }
    }
}
