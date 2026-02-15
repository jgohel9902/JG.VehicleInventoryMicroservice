using JGVehicleInventory.Application.DTOs;
using JGVehicleInventory.Application.Services;
using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Domain.Exceptions;

namespace JGVehicleInventory.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class JGVehiclesController : ControllerBase
{
    private readonly JGCreateVehicleService _createService;
    private readonly JGGetVehicleByIdService _getByIdService;
    private readonly JGGetAllVehiclesService _getAllService;
    private readonly JGUpdateVehicleStatusService _updateStatusService;
    private readonly JGDeleteVehicleService _deleteService;

    public JGVehiclesController(
        JGCreateVehicleService createService,
        JGGetVehicleByIdService getByIdService,
        JGGetAllVehiclesService getAllService,
        JGUpdateVehicleStatusService updateStatusService,
        JGDeleteVehicleService deleteService)
    {
        _createService = createService;
        _getByIdService = getByIdService;
        _getAllService = getAllService;
        _updateStatusService = updateStatusService;
        _deleteService = deleteService;
    }

    // GET /api/vehicles
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JGVehicleResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _getAllService.ExecuteAsync(cancellationToken);
        return Ok(result);
    }

    // GET /api/vehicles/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JGVehicleResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getByIdService.ExecuteAsync(id, cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    // POST /api/vehicles
    [HttpPost]
    public async Task<ActionResult<JGVehicleResponseDto>> Create(
        [FromBody] JGCreateVehicleRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await _createService.ExecuteAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex) when (ex is DomainRuleViolationException || ex is InvalidOperationException)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT /api/vehicles/{id}/status
    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult<JGVehicleResponseDto>> UpdateStatus(
        Guid id,
        [FromBody] JGUpdateVehicleStatusRequestDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _updateStatusService.ExecuteAsync(id, request, cancellationToken);
            if (updated is null) return NotFound();
            return Ok(updated);
        }
        catch (DomainRuleViolationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE /api/vehicles/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _deleteService.ExecuteAsync(id, cancellationToken);
        if (!deleted) return NotFound();
        return NoContent();
    }
}