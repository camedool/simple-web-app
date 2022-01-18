using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Services;

namespace SimpleWebApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemsService;

    public ItemsController(IItemService itemService)
    {
        _itemsService = itemService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemDto>))]
    public async Task<IEnumerable<ItemDto>> Get(CancellationToken cancellationToken)
    {
        return await _itemsService.GetAsync(cancellationToken);
    }

    [HttpPut("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemDto>> Put(int itemId,
        ItemDto itemDto,
        CancellationToken cancellationToken)
    {
        if (itemId != itemDto.Id)
        {
            return BadRequest("Item Id mismatch");
        }

        var item = await _itemsService.UpdateAsync(itemDto, cancellationToken);
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ItemDto> Post(ItemDto itemDto, CancellationToken cancellationToken)
    {
        return await _itemsService.AddAsync(itemDto, cancellationToken);
    }

    [HttpDelete("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int itemId, CancellationToken cancellationToken)
    {
        var isDeleted = await _itemsService.DeleteAsync(itemId, cancellationToken);
        if (isDeleted)
        {
            return NoContent();
        }

        return BadRequest("Couldn't delete item");
    }
}
