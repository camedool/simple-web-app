namespace SimpleWebApp.WebApi.Models;

public sealed class Warehouse : DbItem
{
    private string? _name;
    private string? _location;

    public string Name
    {
        get => _name!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _name = value;
        }
    }

    /// <summary>
    /// Total capacity that is available in warehouse.
    /// For brevity all the good types anticipated 
    /// to occupy equivalent space. 
    /// </summary>
    public long Capacity { get; set; }
    
    /// <summary>
    /// Location of warehouse.
    /// For brevity it's just a free text.
    /// </summary>
    public string Location 
    { 
        get => _location!;
        set 
        {
            ArgumentNullException.ThrowIfNull(value);
            _location = value;
        } 
    }
}
