# TechNova Platform

TechNova is a premium dual-division platform blending enterprise technology services with a modern education academy. This repository contains both the ASP.NET Core 8 Web API backend and the Angular 18 frontend.

## Repository Structure

- `backend/` – Clean architecture-inspired .NET solution featuring REST APIs, EF Core integration, JWT authentication, and Swagger documentation scaffolding.
- `frontend/` – Angular 18 single-page application using standalone components, Angular signals, Lenis smooth scrolling, and GSAP-ready sections for immersive storytelling.

## Getting Started

### Backend
1. Ensure .NET 8 SDK and SQL Server are available.
2. Update `backend/src/API/appsettings.json` with a secure JWT secret and database connection string.
3. From the `backend/` directory run:
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run --project src/API/API.csproj
   ```

### Frontend
1. Install Node.js 18+.
2. From the `frontend/` directory run:
   ```bash
   npm install
   npm start
   ```
3. The app consumes the API at `http://localhost:5000/api` (configure via environment files).

## Features Overview

- Modular services for digital marketing, web/mobile development, and consulting.
- Course catalog with filtering, course detail pages, and enrollment-ready CTAs.
- Student/admin dashboard surfaces analytics, enrollments, notifications, and revenue insights.
- Contact and inquiry workflows bridging the services and education divisions.
- JWT-powered authentication flows for login and registration.
- Responsive, premium UI with glassmorphism, gradients, and motion-ready layout.

## Tooling & Quality

- EF Core repositories and unit-of-work pattern for data persistence.
- Swashbuckle for API documentation.
- Angular reactive forms and interceptors for robust UX.
- Sample unit test coverage ready for expansion via xUnit and Jasmine.

## Deployment

- Package the backend as a containerized ASP.NET app backed by SQL Server.
- Build the Angular app with `npm run build` and serve via a CDN or static hosting.
- Configure CI/CD to run `dotnet test` and `npm test` prior to release.
