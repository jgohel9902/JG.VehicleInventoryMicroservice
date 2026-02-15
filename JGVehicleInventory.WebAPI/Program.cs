using JGVehicleInventory.Application.Interfaces;
using JGVehicleInventory.Infrastructure.Persistence;
using JGVehicleInventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  DbContext
builder.Services.AddDbContext<JGInventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JGInventoryDb")));

//  Repository
builder.Services.AddScoped<JGIVehicleRepository, JGVehicleRepository>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();