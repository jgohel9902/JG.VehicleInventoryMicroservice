using VehicleInventory.Domain.Entities;

namespace JGVehicleInventory.Application.Interfaces;

public interface JGIVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken = default);

    // uniqueness for VehicleCode at application workflow
    Task<bool> ExistsByVehicleCodeAsync(string vehicleCode, CancellationToken cancellationToken = default);
}