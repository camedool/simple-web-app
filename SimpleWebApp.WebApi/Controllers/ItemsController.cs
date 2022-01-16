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
}
