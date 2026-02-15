using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGVehicleInventory.Application.DTOs;
using JGVehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Enums;

namespace JGVehicleInventory.Application.Services;

public sealed class JGUpdateVehicleStatusService
{
    private readonly JGIVehicleRepository _repository;

    public JGUpdateVehicleStatusService(JGIVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<JGVehicleResponseDto?> ExecuteAsync(
        Guid id,
        JGUpdateVehicleStatusRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var vehicle = await _repository.GetByIdAsync(id, cancellationToken);

        if (vehicle is null)
            return null;

        switch (request.Status)
        {
            case VehicleStatus.Available:
                vehicle.MarkAvailable();
                break;

            case VehicleStatus.Reserved:
                vehicle.MarkReserved();
                break;

            case VehicleStatus.Rented:
                vehicle.MarkRented();
                break;

            case VehicleStatus.Serviced:
                vehicle.MarkServiced();
                break;

            default:
                throw new InvalidOperationException("Invalid vehicle status.");
        }

        await _repository.UpdateAsync(vehicle, cancellationToken);

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