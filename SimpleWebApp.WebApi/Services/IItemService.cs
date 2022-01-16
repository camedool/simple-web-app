using SimpleWebApp.WebApi.Dtos;

namespace SimpleWebApp.WebApi.Services;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAsync(
        CancellationToken cancellationToken = default);
    Task<ItemDto> AddAsync(ItemDto itemDto,
        CancellationToken cancellationToken = default);
    Task<ItemDto> UpdateAsync(ItemDto itemDto,
        CancellationToken cancellationToken = default);
}
