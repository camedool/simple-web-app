using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.WebApi.Dtos;
using SimpleWebApp.WebApi.Services;

namespace SimpleWebApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WarehouseDto>))]
    public async Task<IEnumerable<WarehouseDto>> Get(CancellationToken cancellationToken)
    {
        return await _warehouseService.GetAsync(cancellationToken);
    }
}
