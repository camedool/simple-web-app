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

    [HttpPut("{inventoryId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryDto>> Put(int inventoryId, 
        InventoryDto inventoryDto, 
        CancellationToken cancellationToken)
    {
        if (inventoryId != inventoryDto.Id)
        {
            return BadRequest("Inventory Id mismatch");
        }

        var (isUpdated, udpatedInvetory) = await _inventoryService.UpdateAsync(inventoryDto, cancellationToken);
        if (!isUpdated)
        {
            return BadRequest("Can't create an inventory. Insufficient capacity in warehouse you selected. " +
                "Please try another warehouse or first empty some existing inventories.");
        }

        return Ok(udpatedInvetory);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventoryDto>> Post(InventoryDto inventoryDto, CancellationToken cancellationToken)
    {
        var (isCreated, newInventory) = await _inventoryService.AddAsync(inventoryDto, cancellationToken);
        if (!isCreated)
        {
            return BadRequest("Can't create an inventory. Insufficient capacity in warehouse you selected. " +
                "Please try another warehouse or first empty some existing inventories.");
        }

        return Ok(newInventory);
    }

    [HttpDelete("{inventoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int inventoryId, CancellationToken cancellationToken)
    {
        var isDeleted = await _inventoryService.DeleteAsync(inventoryId, cancellationToken);
        if (isDeleted)
        {
            return NoContent();
        }

        return BadRequest("Couldn't delete inventory");
    }
}
