# TechNova Backend

ASP.NET Core 8 Web API following a clean architecture layout. Includes foundational entities for services, courses, enrollment, payments, and content management.

## Projects

- `src/API` – HTTP endpoints, authentication, Swagger.
- `src/Core` – Domain entities, interfaces, enums.
- `src/Infrastructure` – EF Core DbContext, repositories, JWT token service, dependency injection.
- `tests` – xUnit project with starter mapping tests.

## Setup

1. Install .NET 8 SDK and SQL Server.
2. Configure `appsettings.json` with your connection string and JWT secret.
3. From the repository root run:
   ```bash
   dotnet restore TechNova.sln
   dotnet build TechNova.sln
   dotnet run --project src/API/API.csproj
   ```

## Database

- Entity Framework Core with SQL Server provider.
- `ApplicationDbContext` seeds entity relationships for courses, portfolios, services, and blogs.
- Implement migrations using `dotnet ef migrations add InitialCreate` then `dotnet ef database update`.

## Authentication

- JWT authentication configured in `Program.cs`.
- `AuthController` handles registration and login with PBKDF2 password hashing.

## Next Steps

- Implement Identity or custom auth storage with hashed passwords.
- Flesh out controllers with advanced filtering, pagination, and validation.
- Integrate email notifications, payment gateways, and certificate generation.
