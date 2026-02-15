using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGVehicleInventory.Application.Interfaces;

namespace JGVehicleInventory.Application.Services;

public sealed class JGDeleteVehicleService
{
    private readonly JGIVehicleRepository _repository;

    public JGDeleteVehicleService(JGIVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var vehicle = await _repository.GetByIdAsync(id, cancellationToken);

        if (vehicle is null)
            return false;

        await _repository.DeleteAsync(vehicle, cancellationToken);

        return true;
    }
}