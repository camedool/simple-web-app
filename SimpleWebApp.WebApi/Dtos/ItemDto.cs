using SimpleWebApp.WebApi.Enumerations;

namespace SimpleWebApp.WebApi.Dtos;

public sealed class ItemDto
{
    private string? _name;

    public long Id { get; set; }
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
