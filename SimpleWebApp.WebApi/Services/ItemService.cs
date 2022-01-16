using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Extensions;
using SimpleWebApp.WebApi.Repositories;

namespace SimpleWebApp.WebApi.Services;

public sealed class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<ItemDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var items =  await _itemRepository.GetAsync(cancellationToken);
        return items.Select(x => x.ToDto());
    }

    public async Task<ItemDto> AddAsync(ItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var item = itemDto.ToModel();
        await _itemRepository.AddAsync(item, cancellationToken);
        return item.ToDto();
    }

    public async Task<ItemDto> UpdateAsync(ItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var item = itemDto.ToModel();
        await _itemRepository.UpdateAsync(item, cancellationToken);
        return item.ToDto();
    }
}
