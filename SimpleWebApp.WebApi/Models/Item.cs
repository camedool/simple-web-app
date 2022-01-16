using SimpleWebApp.WebApi.Enumerations;

namespace SimpleWebApp.WebApi.Models;

public sealed class Item : DbItem
{
    private string? _name;

    public string Name
    {
        get => _name!;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _name = value;
        }
    }
    public string? Description { get; set; }
    public GoodType Type { get; set; }
}
