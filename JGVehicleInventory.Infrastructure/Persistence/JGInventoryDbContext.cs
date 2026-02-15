using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;

namespace JGVehicleInventory.Infrastructure.Persistence;

public sealed class JGInventoryDbContext : DbContext
{
    public JGInventoryDbContext(DbContextOptions<JGInventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> JG_Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(builder =>
        {
            builder.ToTable("JG_Vehicles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.VehicleCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(v => v.LocationId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(v => v.VehicleType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(v => v.Status)
                   .IsRequired();
            builder.HasIndex(v => v.VehicleCode)
                   .IsUnique();
        });
    }
}