# Contributing to BankRecon

## Critical Implementation Checklist

This document tracks critical items needed to build the BankRecon application following Clean Architecture and Domain-Driven Design principles.

### ✅ Completed Layers

- **Infrastructure Layer** - DbContext, Repository<T>, Entity Configurations, DI Setup

### 🔄 Critical Items to Build (By Priority)

#### Phase 1: Application Layer (Blocking)
- [ ] **Common Models & Exceptions**
  - [ ] Create domain exceptions (e.g., `EntityNotFoundException`, `ValidationException`)
  - [ ] Create API response wrapper models
  - [ ] Create pagination models

- [ ] **MediatR Setup**
  - [ ] Create base command and query handler interfaces
  - [ ] Setup MediatR pipeline behaviors (validation, logging)
  - [ ] Add AutoMapper configuration (for DTO mapping)

- [ ] **DTOs & Validators**
  - [ ] Create request/response DTOs
  - [ ] Create FluentValidation validators
  - [ ] Create AutoMapper profiles

#### Phase 2: WebApi Layer (Blocking)
- [ ] **Program.cs Setup**
  - [ ] Configure dependency injection
  - [ ] Register Infrastructure services
  - [ ] Register Application services
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
  - [ ] Request/response interceptors

- [ ] **Pages & Components**
  - [ ] Layout components (MainLayout, Navigation)
  - [ ] Dashboard page
  - [ ] Entity list pages
  - [ ] Entity detail/edit pages
  - [ ] Entity create pages

- [ ] **State Management (Optional)**
  - [ ] Setup state container (if needed)
  - [ ] Component communication patterns

#### Phase 4: Optional Enhancements
- [ ] Unit Tests (xUnit + Moq)
- [ ] Integration Tests
- [ ] Authentication & Authorization (Identity)
- [ ] Logging (Serilog)
- [ ] Performance Monitoring

### Architecture Notes

- **Domain Layer**: Contains business rules, entities, and domain exceptions
- **Application Layer**: Contains use cases (commands/queries), validators, DTOs, and application services
- **Infrastructure Layer**: ✅ COMPLETE - DbContext, repositories, configurations, and DI setup
- **WebApi Layer**: ASP.NET Core backend, controllers, middleware, configuration
- **Blazor UI Layer**: WebAssembly frontend using MudBlazor components

### Code Style Guidelines

- Follow `.editorconfig` rules strictly (4 spaces, PascalCase for types, camelCase for locals)
- Use file-scoped namespaces
- Enable nullable reference types
- Use expression-bodied members where appropriate
- Add XML documentation for public APIs

### Development Workflow

1. Create domain entities (when design is finalized)
2. Build Application layer use cases
3. Implement WebApi controllers and endpoints
4. Build Blazor UI pages and components
5. Add cross-cutting concerns (logging, error handling)
6. Write tests
7. Performance optimization
