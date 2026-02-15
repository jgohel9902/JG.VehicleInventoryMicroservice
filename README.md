# JG Vehicle Inventory Microservice  (Jenil Gohel, 8909157)
**Assignment 1 – Clean Architecture & DDD**

---

## 1. Project Overview

This project implements the **Inventory bounded context** of a car rental platform using:

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- Clean Architecture
- Domain-Driven Design (DDD)

The service exposes a RESTful CRUD API for managing vehicle inventory while enforcing all business rules inside the Domain layer.

---

## 2. Clean Architecture Structure

The solution contains four layers:

### Domain Layer
- Contains the `Vehicle` aggregate
- Contains `VehicleStatus` enum
- Contains domain exceptions
- No EF Core
- No ASP.NET dependencies

### Application Layer
- Use case services (Create, Get, Update Status, Delete)
- Repository interfaces
- DTOs
- No framework dependencies

### Infrastructure Layer
- EF Core DbContext
- Repository implementation
- Migrations
- Depends on Application layer only
- No business logic

### WebAPI Layer
- Controllers
- Swagger configuration
- Dependency Injection
- Delegates all logic to Application services

---

## 3. Domain Model

### Vehicle Aggregate

Properties:
- Id (Guid)
- VehicleCode
- LocationId
- VehicleType
- Status (VehicleStatus enum)

### Domain Behaviors

- MarkAvailable()
- MarkReserved()
- MarkRented()
- MarkServiced()

All state transitions are validated inside the entity.

---

## 4. Business Rules

- A vehicle cannot be rented if already rented.
- A vehicle cannot be rented if reserved.
- A vehicle cannot be rented if under service.
- A reserved vehicle cannot be marked available without release.
- Invalid transitions throw domain exceptions.

---

## 5. API Endpoints

- GET /api/JGVehicles
- GET /api/JGVehicles/{id}
- POST /api/JGVehicles
- PUT /api/JGVehicles/{id}/status
- DELETE /api/JGVehicles/{id}

Swagger is enabled in Development mode.

---

## 6. Database

Database Name: JGInventoryDb  
Table Name: JG_Vehicles  

Created using EF Core migrations.

---

## 7. Git Usage

The repository contains 30+ meaningful commits reflecting:
- Layer setup
- Domain implementation
- Application services
- Infrastructure configuration
- Migration setup
- Controller implementation
- Swagger testing
- Documentation

---

## 8. How to Run

1. Open solution in Visual Studio
2. Ensure connection string is configured
3. Run the WebAPI project
4. Open Swagger at:
   https://localhost:7237/swagger

---

## 9. Known Limitations

- No authentication
- No pagination/filtering
- Basic DTO validation only