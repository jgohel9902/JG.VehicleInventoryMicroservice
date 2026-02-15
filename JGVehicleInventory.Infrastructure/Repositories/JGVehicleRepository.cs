using JGVehicleInventory.Application.Interfaces;
using JGVehicleInventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;

namespace JGVehicleInventory.Infrastructure.Repositories;

public sealed class JGVehicleRepository : JGIVehicleRepository
{
    private readonly JGInventoryDbContext _context;

    public JGVehicleRepository(JGInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.JG_Vehicles
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JG_Vehicles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        await _context.JG_Vehicles.AddAsync(vehicle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        _context.JG_Vehicles.Update(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        _context.JG_Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByVehicleCodeAsync(string vehicleCode, CancellationToken cancellationToken = default)
    {
        return await _context.JG_Vehicles
            .AnyAsync(v => v.VehicleCode == vehicleCode, cancellationToken);
    }
}