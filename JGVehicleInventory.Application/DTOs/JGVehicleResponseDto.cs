using VehicleInventory.Domain.Enums;

namespace JGVehicleInventory.Application.DTOs;

public sealed class JGVehicleResponseDto
{
    public Guid Id { get; set; }
    public string VehicleCode { get; set; } = string.Empty;
    public string LocationId { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;

    public VehicleStatus Status { get; set; }  
}