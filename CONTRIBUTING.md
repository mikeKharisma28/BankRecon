# Contributing to BankRecon

## Critical Implementation Checklist

This document tracks critical items needed to build the BankRecon application following Clean Architecture and Domain-Driven Design principles.

### ✅ Completed Layers

- **Domain Layer** - BaseEntity, SoftDeletableEntity, Domain Interfaces (IHasKey, ICreatable, IUpdatable, ISoftDeletable)
- **Infrastructure Layer** - DbContext, Repository<T>, Entity Configurations, DI Setup
- **Application Layer** - MediatR CQRS, AutoMapper, FluentValidation, Pipeline Behaviors, DI Setup
- **Shared Layer** - ApiResponse<T>, PaginatedList<T>

### ✅ Completed Items (Phase 1: Application Layer)

- [x] **Common Models & Exceptions**
  - [x] Create domain exceptions (`EntityNotFoundException`, `ValidationException`)
  - [x] Create API response wrapper models (`ApiResponse<T>`, `ApiResponse` in Shared)
  - [x] Create pagination models (`PaginatedList<T>` in Shared)

- [x] **MediatR Setup**
  - [x] Setup MediatR pipeline behaviors (ValidationBehavior, LoggingBehavior)
  - [x] Add AutoMapper configuration (`IMapFrom<T>`, `MappingProfile`)
  - [x] Create `DependencyInjection.AddApplication()` registration

- [x] **DTOs & Validators**
  - [x] Create request/response DTOs (`ExampleSoftDeletableEntityDto`, Create/Update DTOs)
  - [x] Create FluentValidation validators (Create/Update validators)
  - [x] Create AutoMapper profiles via `IMapFrom<T>`

- [x] **CQRS Commands & Queries**
  - [x] GetAll query + handler
  - [x] GetById query + handler
  - [x] Create command + handler
  - [x] Update command + handler
  - [x] Delete command + handler

### 🔄 Critical Items to Build (By Priority)

#### Phase 2: WebApi Layer (Blocking)
- [ ] **Program.cs Setup**
  - [ ] Configure dependency injection
  - [ ] Register Infrastructure services (`AddInfrastructure`)
  - [ ] Register Application services (`AddApplication`)
  - [ ] Setup MediatR
  - [ ] Configure Swagger/OpenAPI
  - [ ] Setup CORS for Blazor frontend

- [ ] **Middleware**
  - [ ] Global exception handling middleware
  - [ ] Request logging middleware
  - [ ] CORS middleware configuration

- [ ] **Configuration**
  - [ ] Create `appsettings.json` with connection strings
  - [ ] Create `appsettings.Development.json`
  - [ ] Setup Entity Framework migrations

- [ ] **API Controllers**
  - [ ] Base controller class
  - [ ] Entity-specific controllers (CRUD endpoints)

#### Phase 3: Blazor UI Layer (Blocking)
- [ ] **Program.cs Setup**
  - [ ] Configure Blazor WebAssembly
  - [ ] Register HttpClient services
  - [ ] Setup MudBlazor components

- [ ] **HTTP Client Services**
  - [ ] Base HTTP client service
  - [ ] Entity-specific API clients

- [ ] **UI Components**
  - [ ] Shared layout components
  - [ ] Entity list pages
  - [ ] Entity form pages (Create/Edit)
  - [ ] Delete confirmation dialogs

### 📁 Project Structure Convention

```
src/BankRecon.Application/
├── Common/
│   ├── Behaviors/           # MediatR pipeline behaviors
│   ├── Exceptions/          # Domain exceptions
│   ├── Interfaces/          # Repository interfaces
│   └── Mappings/            # AutoMapper profiles
├── Features/
│   └── {EntityName}/        # Feature-based organization
│       ├── Commands/
│       │   ├── Create/      # Command + Handler
│       │   ├── Update/      # Command + Handler
│       │   └── Delete/      # Command + Handler
│       ├── Queries/
│       │   ├── GetAll/      # Query + Handler
│       │   └── GetById/     # Query + Handler
│       ├── Dtos/            # Request/Response DTOs
│       └── Validators/      # FluentValidation validators
└── DependencyInjection.cs   # Service registration
```

### 🔧 Coding Standards

- Follow `.editorconfig` rules strictly
- Use file-scoped namespaces
- Use explicit types (avoid `var` except when type is apparent)
- PascalCase for public members, camelCase for private fields
- All entities inherit from `BaseEntity` or `SoftDeletableEntity`
- All CQRS handlers return `ApiResponse<T>` or `ApiResponse`
- All command inputs must have FluentValidation validators
- Use `IMapFrom<T>` interface for AutoMapper DTO mappings

### 🚀 Multi-Project Launch

Both WebApi and Bsui projects can be launched simultaneously using `BankRecon.slnLaunch` (shared) or `BankRecon.slnLaunch.user` (personal):

```json
[
  {
    "Name": "BankRecon",
    "Projects": [
      {
        "Path": "src\\BankRecon.WebApi\\BankRecon.WebApi.csproj",
        "Action": "Start",
        "DebugTarget": "BankRecon.WebApi"
      },
      {
        "Path": "src\\BankRecon.Bsui\\BankRecon.Bsui.csproj",
        "Action": "Start",
        "DebugTarget": "BankRecon.Bsui"
      }
    ]
  }
]
```

| Endpoint | URL |
|---|---|
| WebApi | `https://localhost:57134` |
| Blazor UI | `https://localhost:57123` |
