using JGVehicleInventory.Application.DTOs;
using JGVehicleInventory.Application.Interfaces;

namespace JGVehicleInventory.Application.Services;

public sealed class JGGetVehicleByIdService
{
    private readonly JGIVehicleRepository _repository;

    public JGGetVehicleByIdService(JGIVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<JGVehicleResponseDto?> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var vehicle = await _repository.GetByIdAsync(id, cancellationToken);

        if (vehicle is null)
            return null;

        return new JGVehicleResponseDto
        {
            Id = vehicle.Id,
            VehicleCode = vehicle.VehicleCode,
            LocationId = vehicle.LocationId,
            VehicleType = vehicle.VehicleType,
            Status = vehicle.Status
        };
    }
}