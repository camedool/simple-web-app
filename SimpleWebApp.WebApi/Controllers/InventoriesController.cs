using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Services;

namespace SimpleWebApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoriesController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InventoryDto>))]
    public async Task<IEnumerable<InventoryDto>> Get(CancellationToken cancellationToken)
    {
        return await _inventoryService.GetAsync(cancellationToken);
    }
}
