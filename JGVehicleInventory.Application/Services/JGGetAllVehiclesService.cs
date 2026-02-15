using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGVehicleInventory.Application.DTOs;
using JGVehicleInventory.Application.Interfaces;

namespace JGVehicleInventory.Application.Services;

public sealed class JGGetAllVehiclesService
{
    private readonly JGIVehicleRepository _repository;

    public JGGetAllVehiclesService(JGIVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<JGVehicleResponseDto>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var vehicles = await _repository.GetAllAsync(cancellationToken);

        return vehicles
            .Select(v => new JGVehicleResponseDto
            {
                Id = v.Id,
                VehicleCode = v.VehicleCode,
                LocationId = v.LocationId,
                VehicleType = v.VehicleType,
                Status = v.Status
            })
            .ToList();
    }
}