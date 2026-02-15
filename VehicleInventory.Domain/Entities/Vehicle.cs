using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        // aggregate identity
        public Guid Id { get; private set; }
        public string VehicleCode { get; private set; } = string.Empty;
        public string LocationId { get; private set; } = string.Empty;
        public string VehicleType { get; private set; } = string.Empty;
        public VehicleStatus Status { get; private set; }

        // For EF Core
        private Vehicle() { }

        public Vehicle(Guid id, string vehicleCode, string locationId, string vehicleType)
        {
            if (id == Guid.Empty)
                throw new DomainRuleViolationException("Vehicle Id cannot be empty.");

            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new DomainRuleViolationException("VehicleCode is required.");

            if (string.IsNullOrWhiteSpace(locationId))
                throw new DomainRuleViolationException("LocationId is required.");

            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new DomainRuleViolationException("VehicleType is required.");

            Id = id;
            VehicleCode = vehicleCode.Trim();
            LocationId = locationId.Trim();
            VehicleType = vehicleType.Trim();

            // default lifecycle state
            Status = VehicleStatus.Available;
        }

        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainRuleViolationException("Reserved vehicle cannot be made available without release.");

            Status = VehicleStatus.Available;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainRuleViolationException("Cannot reserve a rented vehicle.");

            if (Status == VehicleStatus.Serviced)
                throw new DomainRuleViolationException("Cannot reserve a serviced vehicle.");

            Status = VehicleStatus.Reserved;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainRuleViolationException("Vehicle already rented.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainRuleViolationException("Vehicle cannot be rented while reserved.");

            if (Status == VehicleStatus.Serviced)
                throw new DomainRuleViolationException("Vehicle cannot be rented while under service.");

            Status = VehicleStatus.Rented;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainRuleViolationException("Rented vehicle cannot be serviced.");

            Status = VehicleStatus.Serviced;
        }

        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainRuleViolationException("Only reserved vehicles can be released.");

            Status = VehicleStatus.Available;
        }
    }
}
