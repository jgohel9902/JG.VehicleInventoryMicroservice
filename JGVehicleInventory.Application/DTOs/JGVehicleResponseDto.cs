using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGVehicleInventory.Application.DTOs
{
    public sealed class JGVehicleResponseDto
    {
        public Guid Id { get; set; }
        public string VehicleCode { get; set; } = string.Empty;
        public string LocationId { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string Status { get; set; }
    }
}
