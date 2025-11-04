# Ctrl.Box - Technical Documentation

[![Build Status](https://julianopestili.visualstudio.com/CtrlBox/_apis/build/status/JulianoMuteki.ctrlbox?branchName=master)](https://julianopestili.visualstudio.com/CtrlBox/_build/latest?definitionId=2&branchName=master)

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Technology Stack](#technology-stack)
4. [Project Structure](#project-structure)
5. [Key Components](#key-components)
6. [Database Design](#database-design)
7. [Security](#security)
8. [Development Setup](#development-setup)
9. [CI/CD](#cicd)
10. [Future Roadmap](#future-roadmap)

## Overview

Ctrl.Box is a comprehensive enterprise application built with **.NET 10.0**, designed as a learning and practice project to master advanced software engineering principles including **Domain-Driven Design (DDD)**, **CQRS**, and **SOLID** principles.

The project focuses on building a robust system for managing business operations including:
- Client and product management
- Inventory and stock control
- Sales and order processing
- Delivery and tracking systems
- Payment management
- Barcode generation and tracking

This is an evolving project with a focus on continuous improvement and learning best practices in software architecture.

## Architecture

The project follows a **Clean Architecture** / **Onion Architecture** pattern with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                     0 - Presentations                        │
│  ┌──────────────────────┐  ┌─────────────────────────────┐ │
│  │   CtrlBox.UI.Web     │  │  CtrlBox.UI.Desktop         │ │
│  │   (ASP.NET MVC Core) │  │  (WPF Desktop Application)  │ │
│  └──────────────────────┘  └─────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
                           ↕
┌─────────────────────────────────────────────────────────────┐
│                       1 - Services                          │
│  ┌───────────────────────────────────────────────────────┐  │
│  │        CtrlBox.Services.Api (Web API)                 │  │
│  │        - Swagger Documentation                         │  │
│  │        - RESTful Endpoints                            │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↕
┌─────────────────────────────────────────────────────────────┐
│                       2 - Application                       │
│  ┌───────────────────────────────────────────────────────┐  │
│  │     CtrlBox.Application                               │  │
│  │     - Application Services                            │  │
│  │     - Business Logic                                  │  │
│  │     - AutoMapper Configuration                        │  │
│  └───────────────────────────────────────────────────────┘  │
│  ┌───────────────────────────────────────────────────────┐  │
│  │     CtrlBox.Application.ViewModel                     │  │
│  │     - View Models                                     │  │
│  │     - DTOs                                            │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↕
┌─────────────────────────────────────────────────────────────┐
│                         3 - Domain                          │
│  ┌───────────────────────────────────────────────────────┐  │
│  │     CtrlBox.Domain                                    │  │
│  │     - Entities                                        │  │
│  │     - Interfaces                                      │  │
│  │     - Validations                                     │  │
│  │     - Domain Logic                                    │  │
│  │     - Identity & Security                             │  │
│  └───────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                           ↕
┌─────────────────────────────────────────────────────────────┐
│                        4 - Infrastructure                   │
│  ┌──────────────────┐  ┌─────────────────────────────────┐  │
│  │   4.1 - Cross    │  │       4.2 - Data                │  │
│  │   Cutting        │  │                                 │  │
│  │   - IoC Container│  │  ┌────────────────────────────┐ │  │
│  │   - Notifications│  │  │ CtrlBox.Infra.Context      │ │  │
│  │   - Barcode      │  │  │ - DbContext                │ │  │
│  │   - Exceptions   │  │  │ - EF Core Mapping          │ │  │
│  │                  │  │  │ - Migrations               │ │  │
│  │                  │  │  └────────────────────────────┘ │  │
│  │                  │  │  ┌────────────────────────────┐ │  │
│  │                  │  │  │ CtrlBox.Infra.Repository   │ │  │
│  │                  │  │  │ - Repositories             │ │  │
│  │                  │  │  │ - Unit of Work             │ │  │
│  │                  │  │  └────────────────────────────┘ │  │
│  └──────────────────┘  └─────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

### Architectural Principles

- **Separation of Concerns**: Each layer has a well-defined responsibility
- **Dependency Inversion**: Higher-level modules don't depend on lower-level modules
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work Pattern**: Manages database transactions
- **Dependency Injection**: Centralized service registration via IoC

## Technology Stack

### Package Versions Summary

The following is a summary of key package versions used across the solution:

| Package | Version | Usage |
|---------|---------|-------|
| .NET Runtime | 10.0 | Core framework |
| Entity Framework Core | 9.0.10 | ORM |
| FluentValidation | 12.1.0 | Validation |
| AutoMapper | 12.0.1 | Object mapping |
| ASP.NET Core Identity | 9.0.10 | Authentication |
| Swashbuckle.AspNetCore | 9.0.6 | API documentation |
| Newtonsoft.Json | 13.0.4 | JSON serialization |
| UnitsNet | 5.75.0 | Unit conversions |
| BarcodeLib | 3.1.5 | Barcode generation |
| Microsoft.Extensions.DependencyInjection | 9.0.10 | IoC container |

### Core Framework
- **.NET 10.0** - Latest .NET runtime
- **ASP.NET Core MVC** - Web application framework
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 9.0** - ORM for database access

### Frontend Technologies
- **Razor Pages** - Server-side rendered pages
- **Bootstrap** - CSS framework for responsive UI
- **jQuery** - JavaScript library
- **WPF (.NET 10.0)** - Modern desktop application

### Authentication & Authorization
- **ASP.NET Core Identity** - User management and authentication
- **Custom Claims-based Authorization** - Fine-grained permission system

### Data Access & Mapping
- **Entity Framework Core 9.0.10** - ORM
- **FluentValidation 12.1.0** - Object validation library
- **AutoMapper 12.0.1** - Object-to-object mapping

### Additional Libraries
- **Swashbuckle.AspNetCore 9.0.6** - API documentation (Swagger/OpenAPI)
- **Newtonsoft.Json 13.0.4** - JSON serialization
- **UnitsNet 5.75.0** - Unit conversion and measurement utilities
- **BarcodeLib 3.1.5** - Barcode generation library

### Infrastructure
- **Microsoft SQL Server** - Primary database
- **Docker** - Containerization
- **Azure DevOps** - CI/CD pipeline

## Project Structure

### Presentation Layer (0 - Presentations)

#### CtrlBox.UI.Web
ASP.NET Core MVC web application providing the main user interface.

**Key Features:**
- User authentication and authorization
- Client management interface
- Product catalog management
- Sales and order processing
- Delivery tracking
- Stock management
- Payment processing

**Dependencies:**
- Controllers (14 controllers)
- Views (78 Razor views)
- Models (18 models)
- Extensions and helpers
- AutoMapper for DTO mapping

#### CtrlBox.UI.Desktop
WPF desktop application targeting .NET 10.0-windows7.0.

**Key Features:**
- Modern .NET 10.0 implementation
- Desktop client interface
- Theme customization
- User controls
- Version 1.0.0.0

**Technology:**
- Target Framework: `net10.0-windows7.0`
- Uses WPF (Windows Presentation Foundation)
- Nullable reference types enabled
- Implicit usings enabled

#### CtrlBox.UI.Desktop.NET8
Alternative WPF desktop application targeting .NET 8.0-windows (maintained separately).

### Services Layer (1 - Services)

#### CtrlBox.Services.Api
RESTful Web API providing backend services.

**Key Features:**
- Swagger UI documentation (`/swagger`)
- XML API documentation
- RESTful endpoints
- Stateless API design

**Configuration:**
- Configured with Swagger/OpenAPI
- SQL Server database connection
- Dependency injection setup
- AutoMapper integration

### Application Layer (2 - Application)

#### CtrlBox.Application
Contains application services and business logic orchestration.

**Services:**
- `AddressApplicationService`
- `BoxApplicationService`
- `ClientApplicationService`
- `ConfigurationApplicationService`
- `DeliveryApplicationService`
- `PaymentApplicationService`
- `ProductApplicationService`
- `RouteApplicationService`
- `SaleApplicationService`
- `SecurityApplicationService`
- `TrackingApplicationService`

**AutoMapper Profiles:**
- 28 mapping profiles for entity-to-DTO transformations
- Centralized configuration
- Profile separation by domain

#### CtrlBox.Application.ViewModel
Contains data transfer objects and view models.

**Key View Models:**
- AddressVM, ClientVM, ProductVM
- BoxVM, BoxTypeVM, BoxProductItemVM
- OrderVM, SaleVM, PaymentVM
- StockVM, StockMovementVM
- RouteVM, DeliveryDetailVM
- TrackingVM, TrackingClientVM
- And more...

#### CtrlBox.AppService
Application service layer (additional abstraction).

### Domain Layer (3 - Domain)

#### CtrlBox.Domain
Core domain entities and business logic.

**Entities Organized by Domain:**
- **Registration**: `Address`, `Client`
- **Product Management**: `Product`, `ProductItem`, `Box`, `BoxType`
- **Stock**: `Stock`, `StockMovement`
- **Orders**: `Order`, `OrderBox`, `OrderProductItem`
- **Delivery**: `Shipment`, `ShipmentBox`, `DeliveryDetail`, `DeliveryBox`
- **Transport**: `Route`
- **Sales**: `Sale`, `Payment`, `PaymentMethod`, `PaymentSchedule`
- **Tracking**: `Tracking`, `TrackingType`, `TrackingClient`, `TrackingBox`
- **Value Objects**: Client-specific configurations and options
- **SubEntities**: Supporting entities like `BoxType`, `GraphicCodes`, `OptiontType`, `Picture`, `TrackingType`
- **Aggregation**: `FlowStep`

**Total Entities:** 37+

**Additional Components:**
- `Common` - Base classes and shared logic
- `Interfaces` - Repository and service contracts (25+ interfaces)
- `Validations` - FluentValidation validators (7+ validators)
- `Identity` - ASP.NET Identity customization (7 classes)
- `Security` - Claims and authorization (5 classes)

### Infrastructure Layer (4 - Infrastructure)

#### 4.1 - CrossCutting

##### CtrlBox.CrossCutting
Cross-cutting concerns and shared utilities.

**Components:**
- `NotificationContext` - Domain notifications
- `CustomException` - Custom exception types
- `Enums` - Domain enumerations (6+ enums)
- `CtrlBoxUnits` - Unit conversion utilities

**Packages:**
- FluentValidation 12.1.0
- Newtonsoft.Json 13.0.4
- UnitsNet 5.75.0 - Unit conversion and measurement library

##### CtrlBox.CrossCutting.Ioc
Dependency injection configuration.

**Modules:**
- `ApplicationBootStrapperModule` - Application services registration
- `InfraBootStrapperModule` - Infrastructure services registration

##### CtrlBox.CrossCutting.Barcode
Barcode generation utilities.

**Components:**
- `BarcodeGenerator` - Barcode creation and management

**Packages:**
- BarcodeLib 3.1.5 - Barcode generation library

#### 4.2 - Data

##### CtrlBox.Infra.Context
Entity Framework Core database context and configurations.

**Key Features:**
- `CtrlBoxContext` - Main DbContext
- `DbInitializer` - Database seeding
- 35+ Fluent API mapping configurations
- Identity integration with custom user and role types
- Query optimization with NoTracking behavior
- Transaction management

**DbSets:**
- Identity: ApplicationUser, ApplicationRole (custom implementations)
- Core: Addresses, Clients, Products, Boxes
- Orders: Orders, OrderBoxes, OrderProductItems
- Delivery: DeliveriesDetails, Routes
- Sales: Sales, SalesProducts, RoutesClients
- Tracking: Trackings, TrackingsTypes, TrackingsClients, TrackingsBoxes
- Stock: Stocks, StocksMovements
- Payment: Payments, PaymentMethods, PaymentSchedules
- Configuration: OptiontsTypes, ClientsOptionsTypes, Pictures, SystemConfigurations

##### CtrlBox.Infra.Repository
Repository pattern implementation.

**Components:**
- Generic repository pattern
- `UnitOfWork` - Transaction management
- 9+ specialized repositories
- Common repository utilities

## Key Components

### Dependency Injection

Services are registered in two bootstrapper modules:

```csharp
// Infrastructure services
InfraBootStrapperModule.RegisterServices(services);

// Application services  
ApplicationBootStrapperModule.RegisterServices(services);
```

This centralizes dependency configuration and keeps it isolated from presentation layers.

### Unit of Work Pattern

The `IUnitOfWork` interface provides:
- Generic repository access
- Custom repository support
- Synchronous commit operations
- Rollback capabilities
- Tracking control

### Identity System

Custom ASP.NET Core Identity implementation:
- `ApplicationUser` - Extended user model
- `ApplicationRole` - Role management
- Custom claims-based authorization
- 30-minute lockout policy
- Strong password requirements (8+ chars, special chars, etc.)

### Authorization Policies

Fine-grained permission system:
- Custom claims-based policies
- Policy registration from centralized list
- Integration with ASP.NET Core authorization middleware

### AutoMapper Configuration

Centralized object mapping:
- Profile-based configuration
- Organized by domain
- Used across all layers for DTO transformations

### Notification System

Domain-driven notification pattern:
- `NotificationContext` for domain events
- Validation result notifications
- Business rule violation notifications

## Database Design

### Primary Database
- **SQL Server 2019** (via Docker or local instance)

### Docker Setup

```bash
# Start Docker service
sudo service docker start

# Pull SQL Server image
sudo docker pull mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

# Run SQL Server container
sudo docker run -e 'ACCEPT_EULA=Y' \
  -e 'SA_PASSWORD=YourStrong!Passw0rd' \
  --network=mssql-network \
  -e 'MSSQL_PID=Developer' \
  -p 11433:1433 \
  -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

# Alternative: Use port 14333 (update connection string accordingly)
# -p 14333:1433 \

# Get container IP
ip addr show | grep inet
```

### Connection Configuration

Configuration stored in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<server>;Database=CtrlBoxDB;..."
  }
}
```

### Entity Framework Migrations

EF Core migrations managed in `CtrlBox.Infra.Context/Migrations/`

Current migrations: 3+

### Database Seeding

`DbInitializer` class handles initial data population for development and testing.

## Security

### Authentication
- ASP.NET Core Identity
- Cookie-based authentication
- Secure password hashing
- Email confirmation support (via `ICustomEmailSender`)

### Authorization
- Role-based authorization
- Claims-based authorization
- Policy-based authorization
- Custom permission claims

### Password Policy
- Minimum length: 8 characters
- Requires uppercase, lowercase, digits
- Requires non-alphanumeric characters
- Requires unique characters: 7

### Lockout Policy
- Max failed attempts: 5
- Lockout duration: 30 minutes
- New users subject to lockout

### Additional Security Features
- HTTPS enforcement in production
- HSTS (HTTP Strict Transport Security)
- Cookie policy configuration
- CSRF protection (built-in MVC)

## Development Setup

### Prerequisites
- **.NET 10.0 SDK** or later
- **Visual Studio 2022** or **VS Code**
- **SQL Server 2019** (or Docker)
- **Git**

### Clone and Build

```bash
# Clone repository
git clone <repository-url>
cd ctrlbox

# Restore NuGet packages
dotnet restore

# Build solution
dotnet build CtrlBox.sln

# Run database migrations (if needed)
cd src/CtrlBox.Infra.Context
dotnet ef database update

# Run web application
cd src/CtrlBox.UI.Web
dotnet run
```

### Configuration

Update `appsettings.Development.json` with your database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost,11433; database=CtrlBox;Trusted_Connection=False;User Id=sa;Password=YourStrong!Passw0rd;MultipleActiveResultSets=True;"
  }
}
```

**Note:** Default connection uses port `11433` (or `14333` for Docker setup). Adjust the port according to your SQL Server configuration.

### Running Different Layers

**Web UI:**
```bash
cd src/CtrlBox.UI.Web
dotnet run
```

**Web API:**
```bash
cd src/CtrlBox.Services.Api
dotnet run
```

**Desktop Application (.NET 10.0):**
```bash
cd src/CtrlBox.UI.Desktop
dotnet run
```

**Desktop Application (.NET 8.0 - alternative):**
```bash
cd src/CtrlBox.UI.Desktop.NET8
dotnet run
```

### Access Points
- Web UI: `https://localhost:5001` (or configured port)
- Web API: `https://localhost:5001/api` (or configured port)
- Swagger UI: `https://localhost:5001/swagger`

## CI/CD

### Azure DevOps Pipeline

Configuration file: `azure-pipelines.yml`

**Pipeline Configuration:**
- **Trigger**: Master branch
- **Agent**: Windows latest
- **Build Platform**: Any CPU
- **Build Configuration**: Release

**Pipeline Steps:**
1. NuGet tool installer
2. NuGet restore
3. Visual Studio Build
4. VSTest (Unit testing)

**Build Artifacts:**
- Web application ZIP package
- Desktop application binaries
- Deployment-ready artifacts

**Deployment:**
- IIS deployment configured
- Package as single file
- Web publish method: Package

## Future Roadmap

### Planned Features
- [ ] **Azure DevOps** integration (build pipeline configured)
- [x] **API ASP.NET Core** (partial implementation)
- [x] **ASP.NET MVC Core** (implemented)
- [x] **FluentValidation 12.1.0** (installed and in use)
- [x] **Entity Framework Core** (implemented)
- [x] **Swagger** (implemented)
- [ ] **RabbitMQ** - Message queue integration
- [x] **SQL Server** (implemented)
- [ ] **MongoDB/Postgres** - Alternative database support
- [x] **Docker** (container configuration)
- [ ] **Azure** - Cloud deployment
- [ ] **Tests** - Unit, integration, and E2E tests

### Learning Goals
- **DDD (Domain-Driven Design)**
  - ✅ Proper entity organization
  - ✅ Value objects implementation
  - ✅ Aggregation roots
  - ⏳ Ubiquitous language enforcement
  - ⏳ Domain events

- **CQRS (Command Query Responsibility Segregation)**
  - ⏳ Separate read/write models
  - ⏳ Command handlers
  - ⏳ Query handlers
  - ⏳ Event sourcing

- **SOLID Principles**
  - ✅ Single Responsibility
  - ✅ Dependency Inversion
  - ⏳ Interface Segregation refinement
  - ⏳ Open/Closed enhancement

### Additional Improvements
- Expand test coverage
- Implement MediatR for CQRS
- Add Redis caching layer
- Implement GraphQL endpoint
- Real-time updates with SignalR
- Mobile application (Xamarin/MAUI)
- Advanced analytics and reporting
- Document management system

## Project Status

**Current Version:** .NET 10.0  
**Status:** Active Development  
**Target:** Production-ready learning platform

### Recent Updates

**Major Version Upgrade:**
- ✅ Migrated from .NET 8.0 to **.NET 10.0** across all projects
- ✅ Desktop application upgraded from legacy .NET Framework to .NET 10.0
- ✅ Entity Framework Core updated to **9.0.10**
- ✅ FluentValidation updated to **12.1.0**
- ✅ AutoMapper updated to **12.0.1**
- ✅ Added **UnitsNet** library for unit conversions
- ✅ Updated ASP.NET Core Identity to **9.0.10**
- ✅ Swashbuckle (Swagger) updated to **9.0.6**

### Known Issues
- Some bugs present (as documented in original README)
- Incomplete test coverage
- Some features under construction
- Backup project file (`CtrlBox.UI.Desktop.csproj.backup`) indicates migration from .NET Framework to .NET 10.0

### Contributing

This is a learning project open to suggestions and improvements. Key areas for contribution:
- Bug fixes
- Test implementations
- Documentation improvements
- Code quality enhancements
- Feature additions

## License

[To be determined]

## Contact & Resources

- **Azure DevOps**: https://julianopestili.visualstudio.com/CtrlBox
- **Build Pipeline**: [![Build Status](https://julianopestili.visualstudio.com/CtrlBox/_apis/build/status/JulianoMuteki.ctrlbox?branchName=master)](https://julianopestili.visualstudio.com/CtrlBox/_build/latest?definitionId=2&branchName=master)

---

**Note:** This is a learning project focused on mastering enterprise software architecture patterns and best practices. The architecture and design decisions reflect ongoing learning and experimentation.

