using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;

namespace JGVehicleInventory.Application.DTOs
{
    public sealed class JGUpdateVehicleStatusRequestDto
    {
        public VehicleStatus Status { get; set; }
    }
}
