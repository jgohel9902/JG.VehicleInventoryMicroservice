using JGVehicleInventory.Application.DTOs;
using JGVehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;

namespace JGVehicleInventory.Application.Services;

public sealed class JGCreateVehicleService
{
    private readonly JGIVehicleRepository _repository;

    public JGCreateVehicleService(JGIVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<JGVehicleResponseDto> ExecuteAsync(
        JGCreateVehicleRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (await _repository.ExistsByVehicleCodeAsync(request.VehicleCode, cancellationToken))
            throw new InvalidOperationException("Vehicle with same code already exists.");

        var vehicle = new Vehicle(
            Guid.NewGuid(),
            request.VehicleCode,
            request.LocationId,
            request.VehicleType
        );

        await _repository.AddAsync(vehicle, cancellationToken);

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