# BankRecon

A modern bank reconciliation application built with **Clean Architecture** and **Domain-Driven Design (DDD)** principles using .NET 8, Blazor WebAssembly, and Entity Framework Core.

## 📋 Overview

BankRecon is a comprehensive solution for bank account reconciliation, enabling users to:
- Manage multiple bank accounts
- Track financial transactions
- Reconcile bank statements with ledger records
- Maintain complete audit trails with soft delete capabilities
- Access data through a responsive Blazor WebAssembly UI

## 🏗️ Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:


┌─────────────────────────────────────────┐
│     BankRecon.Bsui                      │
│  (Blazor WebAssembly UI - MudBlazor)   │
└────────────┬────────────────────────────┘
             │
┌────────────▼────────────────────────────┐
│     BankRecon.WebApi                    │
│  (ASP.NET Core - Controllers/Endpoints) │
└────────────┬────────────────────────────┘
             │
┌────────────▼────────────────────────────┐
│   BankRecon.Application                 │
│  (MediatR Handlers - CQRS Pattern)      │
└────────────┬────────────────────────────┘
             │
  ┌──────────┼──────────┐
  │          │          │
┌─▼──────┐ ┌─▼────────┐ ┌─▼────────────┐
│ Domain │ │Shared   │ │Infrastructure│
│ (DDD)  │ │(DTOs)   │ │(EF Core, DB) │
└────────┘ └─────────┘ └──────────────┘


### Layers

- **Domain Layer** (`BankRecon.Domain`)
  - Core business entities and DDD concepts
  - Interface-driven architecture (ICreatable, IUpdatable, ISoftDeletable)
  - No external dependencies

- **Application Layer** (`BankRecon.Application`)
  - MediatR-based CQRS implementation
  - Use cases as Commands and Queries
  - DTOs and validation rules
  - AutoMapper profiles

- **Infrastructure Layer** (`BankRecon.Infrastructure`) ✅
  - Entity Framework Core with SQL Server
  - Generic Repository pattern
  - Database configurations and migrations
  - Dependency injection setup

- **WebApi Layer** (`BankRecon.WebApi`)
  - ASP.NET Core REST API endpoints
  - Global exception handling
  - Swagger/OpenAPI documentation
  - CORS configuration for Blazor

- **Blazor UI Layer** (`BankRecon.Bsui`)
  - Blazor WebAssembly frontend
  - MudBlazor component library
  - HttpClient services for API communication
  - Responsive and modern UI

- **Shared Layer** (`BankRecon.Shared`)
  - Shared DTOs and models
  - Validation rules
  - Common utilities

## 🚀 Tech Stack

- **.NET 8**
- **ASP.NET Core** - Web API framework
- **Blazor WebAssembly** - Frontend framework
- **Entity Framework Core 8** - ORM with SQL Server
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Validation library
- **AutoMapper** - Object mapping
- **MudBlazor** - Material Design component library
- **Swagger/OpenAPI** - API documentation

## 📦 Project Structure


src/
├── BankRecon.Domain/              # Core domain entities and interfaces
│   ├── Common/
│   │   ├── BaseEntity.cs
│   │   ├── AuditableEntity.cs
│   │   └── Interfaces/             # IAuditable, ICreatable, etc.
│   └── Entities/                   # Business entities
├── BankRecon.Application/          # Application services and use cases
│   ├── Common/
│   │   └── Interfaces/
│   │       └── IRepository.cs
│   ├── Features/                   # Commands, Queries, Handlers
│   ├── DTOs/
│   ├── Validators/
│   └── MappingProfiles/
├── BankRecon.Infrastructure/       # Data access and external services
│   ├── Data/
│   │   └── BankReconDbContext.cs
│   ├── Repositories/
│   │   └── Repository.cs
│   ├── Configurations/             # Entity Framework configurations
│   └── DependencyInjection.cs
├── BankRecon.WebApi/               # REST API endpoints
│   ├── Controllers/
│   ├── Middleware/
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
├── BankRecon.Bsui/                 # Blazor WebAssembly frontend
│   ├── Pages/
│   ├── Components/
│   ├── Services/
│   └── Program.cs
└── BankRecon.Shared/               # Shared models and utilities
    ├── DTOs/
    ├── Models/
    └── Validators/


## ✨ Key Features

### Infrastructure Layer ✅
- **Generic Repository Pattern** - Reusable data access abstraction
- **Soft Delete Support** - Mark entities as deleted without removing data
- **Audit Trail** - Track creation, updates, and deletions
- **Query Filters** - Automatic exclusion of soft-deleted entities
- **EF Core Configurations** - Type-safe entity mapping and relationships

### Entity Flexibility

// BaseEntity - Basic audit tracking (default)
public class BankAccount : BaseEntity { }

// AuditableEntity - Full audit trail with soft delete
public class Transaction : AuditableEntity { }


## 🔧 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or remote)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**

   git clone https://github.com/mikeKharisma28/BankRecon.git
   cd BankRecon


2. **Setup database connection**
- Edit `src/BankRecon.WebApi/appsettings.Development.json`
- Update connection string for your SQL Server instance:

   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=BankReconDb;Trusted_Connection=true;"
     }
   }


3. **Create database and migrations**

   cd src/BankRecon.WebApi
   dotnet ef database update --project ../BankRecon.Infrastructure


4. **Build and run**

   # Run WebApi
   cd src/BankRecon.WebApi
   dotnet run

   # Run Blazor UI (in separate terminal)
   cd src/BankRecon.Bsui
   dotnet run


## 📚 Development Workflow

### Creating a New Feature

1. **Create domain entity** (if needed)

   public class MyEntity : AuditableEntity
   {
       public string Name { get; set; } = string.Empty;
   }


2. **Create entity configuration**

   public class MyEntityConfiguration : AuditableEntityConfiguration<MyEntity>
   {
       public override void Configure(EntityTypeBuilder<MyEntity> builder)
       {
           base.Configure(builder);
           builder.ToTable("MyEntities");
           // Configure properties
       }
   }


3. **Create DTOs and validators**
4. **Create MediatR handlers** (Commands/Queries)
5. **Create API controllers**
6. **Create Blazor pages and components**

## 🎯 Critical Implementation Checklist

### ✅ Completed
- Infrastructure Layer (DbContext, Repository, Configurations, DI)

### 🔄 In Progress / Todo
- [ ] Application Layer (MediatR handlers, DTOs, validators)
- [ ] WebApi Layer (Controllers, middleware, endpoints)
- [ ] Blazor UI Layer (Pages, components, services)
- [ ] Authentication & Authorization
- [ ] Unit & Integration Tests
- [ ] Logging (Serilog)

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed implementation checklist.

## 🔐 Code Standards

This project enforces strict code style via `.editorconfig`:
- 4-space indentation
- UTF-8 encoding with CRLF line endings
- PascalCase for types and methods
- camelCase for local variables and private fields
- File-scoped namespaces
- Nullable reference types enabled

## 📖 Resources

- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://www.domainlanguage.com/ddd/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [MudBlazor Components](https://mudblazor.com/)

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

- **Mike Kharisma** - [@mikeKharisma28](https://github.com/mikeKharisma28)

## 🤝 Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to contribute to this project.

## 📞 Support

For issues, questions, or suggestions, please [open an issue](https://github.com/mikeKharisma28/BankRecon/issues) on GitHub.

---

**Last Updated**: April 2026 
**Status**: 🚧 Under Development - Infrastructure Layer Complete


This version maintains the original structure while enhancing clarity and coherence, ensuring that all relevant information is presented effectively.
