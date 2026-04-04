# Contributing to BankRecon

Thank you for your interest in contributing to BankRecon! This document outlines the project structure, development guidelines, and contribution workflow.

## 📋 Development Phases & Status

### ✅ Phase 1: Core Architecture & Application Layer (COMPLETED)

#### Domain Layer
- [x] Base entities (`BaseEntity`, `SoftDeletableEntity`)
- [x] Domain interfaces (`IHasKey`, `ICreatable`, `IUpdatable`, `ISoftDeletable`)
- [x] Removed external dependencies (Domain layer is clean)
- [x] Audit Log entity (`AuditLog`)

#### Shared Layer
- [x] API response wrapper (`ApiResponse<T>`)
- [x] Pagination model (`PaginatedList<T>`)
- [x] Mapping interface (`IMapFrom<T>`)
- [x] Minimized dependencies (only AutoMapper + Domain)
- [x] AuditLog DTO (`AuditLogDto`)

#### Application Layer
- [x] Domain exceptions (`EntityNotFoundException`, `ValidationException`)
- [x] MediatR CQRS setup with command/query handlers
- [x] FluentValidation validators
- [x] AutoMapper configuration with reflection-based discovery
- [x] Pipeline behaviors (`ValidationBehavior`, `LoggingBehavior`)
- [x] DependencyInjection registration (`AddApplication()`)
- [x] Audit Log queries (`GetAllAuditLogsQuery`, `GetAuditLogsByEntityQuery`)

#### Infrastructure Layer
- [x] DbContext (`BankReconDbContext`)
- [x] Generic `Repository<T>` implementation
- [x] Specialized `IAuditLogRepository` for audit queries
- [x] Entity configurations (Fluent API)
- [x] AuditLog configuration (`AuditLogConfiguration`)
- [x] DependencyInjection registration (`AddInfrastructure()`)
- [x] Soft delete query filtering
- [x] Automatic audit log system with change tracking via `SaveChangesAsync()` override

### ✅ Phase 2: WebApi Layer (COMPLETED)

#### Program.cs & Middleware
- [x] Dependency injection registration (Application + Infrastructure)
- [x] Swagger/OpenAPI configuration
- [x] CORS setup for Blazor WebAssembly (correct policy name + matching ports)
- [x] Exception handling middleware (`ExceptionHandlingMiddleware`)
- [x] Correct middleware ordering (`UseCors` before `UseAuthorization`)

#### Controllers
- [x] Audit Logs controller (`AuditLogsController`)
- [x] REST endpoint implementations for audit querying
- [x] Proper HTTP status codes
- [x] XML documentation for Swagger

#### Configuration
- [x] `appsettings.json` (base configuration)
- [x] `appsettings.Development.json` (development overrides)
- [x] Connection string setup
- [x] Logging configuration

### ✅ Phase 3: Blazor WebAssembly Client (COMPLETED)

#### Bsui.Client Layer (API Client Infrastructure)
- [x] Project scaffolded (`BankRecon.Bsui.Client`)
- [x] Base API client interface (`IApiClient`) in `Common/Interfaces/`
- [x] Base API client implementation (`ApiClient`) in `Common/Services/`
- [x] Typed HttpClient with `IHttpClientFactory` via `AddHttpClient<IApiClient, ApiClient>()`
- [x] Feature service interfaces separated into `Features/Interfaces/`
  - [x] `IAuditLogService`
- [x] Feature service implementations separated into `Features/Services/`
  - [x] `AuditLogService`
- [x] DependencyInjection extension (`AddBsuiClient(string baseAddress)`)

### ✅ Phase 4: Bsui Foundation & Audit Log UI (COMPLETED)

#### Bsui Foundation
- [x] `wwwroot/index.html` — correct WASM host entry point with MudBlazor assets and loading indicator
- [x] `App.razor` — clean Router component (separated from HTML host page)
- [x] `MainLayout.razor` — MudBlazor layout (`MudThemeProvider`, `MudDialogProvider`, `MudSnackbarProvider`)
- [x] `Program.cs` — environment-aware API base URL (dev: hardcoded WebApi port, prod: `BaseAddress`)
- [x] `_Imports.razor` — global usings for MudBlazor, Shared, and Bsui.Client namespaces

#### Audit Log UI Pages
- [x] `Features/AuditLogs/_Imports.razor` — feature-scoped `IAuditLogService` injection
- [x] `Features/AuditLogs/Index.razor` — list page with `MudDataGrid`, action color chips, loading skeleton, snackbar error handling
- [x] `Features/AuditLogs/Detail.razor` — detail page with formatted JSON old/new values, `MudField` display

#### Home Page
- [x] `Features/Home.razor` — dashboard landing page (`@page "/"`)

### 📍 Phase 5: Remaining UI Features (IN PROGRESS)

#### Navigation & Layout (NEXT)
- [ ] `NavMenu.razor` — Navigation menu component with MudNavMenu
- [ ] Drawer layout integration in `MainLayout.razor`
- [ ] Responsive navigation for mobile/desktop
- [ ] Active route highlighting

#### Entity Management Pages (UPCOMING)
- [ ] Generic list page template with sorting, filtering, pagination
- [ ] Generic detail view with formatted display
- [ ] Create/Edit form pages with MudForm validation
- [ ] Client-side form validation with error messages
- [ ] Success/error snackbar notifications

#### Global State Management (PLANNED)
- [ ] State container service for shared component state
- [ ] Event notification system for cross-component communication
- [ ] Cascading parameters for component hierarchy

#### Advanced UI Features (PLANNED)
- [ ] Error handling UI components (error boundaries, custom error pages)
- [ ] Loading indicators and skeleton screens
- [ ] Modal dialogs for confirmations
- [ ] Bulk operations UI
- [ ] Export/Import functionality

#### Advanced Backend Features (PLANNED)
- [ ] Authentication and Authorization (JWT)
- [ ] Role-based access control (RBAC)
- [ ] Advanced search & filtering
- [ ] PerformedBy user identity population
- [ ] Unit & integration tests
- [ ] Performance optimization

---

## 🏗️ Architecture Guidelines

### Clean Architecture Principles

```
Dependency Direction: Outer layers → Inner layers
Domain layer has NO external dependencies
Each layer has single responsibility
```

### Layer Responsibilities

| Layer | Responsibility | Can Reference |
|-------|-----------------|----------------|
| **Domain** | Business logic, entities | Nothing (pure) |
| **Application** | Use cases, orchestration | Domain, Shared |
| **Infrastructure** | Data access, external services | Application, Domain |
| **Shared** | DTOs, common models | Domain |
| **WebApi** | HTTP handling, routing | Application, Infrastructure, Shared |
| **Bsui.Client** | HTTP client services, API communication | Shared |
| **Bsui** | UI rendering, user interaction | Bsui.Client, Shared |

### CQRS Pattern

- **Commands** — State-changing operations (Create, Update, Delete)
- **Queries** — Read-only operations (GetAll, GetById)
- **Handlers** — Business logic execution
- **Validators** — Input validation before handler execution

---

## 🛠️ Development Guidelines

### Code Style & Formatting

Follow the `.editorconfig` file in the root directory. Key rules:

```
• Indentation: 4 spaces (no tabs)
• Line endings: CRLF
• Naming: PascalCase for classes/methods, camelCase for variables
• File-scoped namespaces: Required
• Braces: Allman style (opening brace on new line)
```

Run formatting:
```bash
dotnet format
```

### Adding New Features

1. **Create Domain Entity** (if needed)
   ```
   src/BankRecon.Domain/Entities/YourEntity.cs
   ```

2. **Create DTOs in Shared**
   ```
   src/BankRecon.Shared/Features/YourFeature/Dtos/
   ```

3. **Implement `IMapFrom<T>`** in DTO
   ```csharp
   public class YourDto : IMapFrom<YourEntity>
   {
       public void Mapping(Profile profile)
       {
           profile.CreateMap<YourEntity, YourDto>();
       }
   }
   ```

4. **Create Application Commands/Queries**
   ```
   src/BankRecon.Application/Features/YourFeature/
   ├── Commands/
   ├── Queries/
   └── Validators/
   ```

5. **Create Controller Endpoint**
   ```
   src/BankRecon.WebApi/Controllers/YourController.cs
   ```

6. **Create Client Service Interface**
   ```
   src/BankRecon.Bsui.Client/Features/Interfaces/IYourFeatureService.cs
   ```

7. **Create Client Service Implementation**
   ```
   src/BankRecon.Bsui.Client/Features/Services/YourFeatureService.cs
   ```

   Register in `src/BankRecon.Bsui.Client/DependencyInjection.cs`:
   ```csharp
   services.AddScoped<IYourFeatureService, YourFeatureService>();
   ```

8. **Create Blazor Pages**
   ```
   src/BankRecon.Bsui/Features/YourFeature/
   ├── _Imports.razor       ← inject IYourFeatureService here
   ├── Index.razor
   └── Detail.razor      ← detail page
   ```

   Add feature-scoped service injection in `_Imports.razor`:
   ```razor
   @using BankRecon.Bsui.Client.Features.Interfaces
   @using BankRecon.Shared.Features.YourFeature.Dtos
   @inject IYourFeatureService YourFeatureService
   ```

### Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| **Command** | `{Action}{EntityName}Command` | `CreateTransactionCommand` |
| **Query** | `Get{EntityName}{Criteria}Query` | `GetAllTransactionsQuery` |
| **Handler** | `{Command/Query}Handler` | `CreateTransactionCommandHandler` |
| **Validator** | `{Command}Validator` | `CreateTransactionValidator` |
| **DTO** | `{EntityName}Dto` | `TransactionDto` |
| **Entity** | `{EntityName}` | `Transaction` |
| **Repository** | `I{EntityName}Repository` | `ITransactionRepository` |
| **Service** | `I{FeatureName}Service` | `ITransactionService` |

---

## 🌐 Bsui.Client Layer

### Layer Structure

```
BankRecon.Bsui.Client/
├── Common/
│   ├── Interfaces/
│   │   └── IApiClient.cs                # Base HTTP client contract
│   └── Services/
│       └── ApiClient.cs                 # Base HTTP client implementation
├── Features/
│   ├── Interfaces/                      # All feature service contracts
│   │   └── IAuditLogService.cs
│   └── Services/                        # All feature service implementations
│       └── AuditLogService.cs
├── BankRecon.Bsui.Client.csproj
└── DependencyInjection.cs
```

> **Note:** Interfaces and implementations are intentionally separated into `Features/Interfaces/` and `Features/Services/` to maintain Clean Architecture separation of concerns.

### Guidelines for New Services

1. Create interface in `Features/Interfaces/I{FeatureName}Service.cs`
2. Create implementation in `Features/Services/{FeatureName}Service.cs`
3. Register in `DependencyInjection.cs`
4. Add `@using BankRecon.Bsui.Client.Features.Interfaces` to feature `_Imports.razor`

---

## 🖥️ Bsui Layer

### Key Setup Notes

- **`wwwroot/index.html`** is the WASM host — this is where MudBlazor CSS/JS and `blazor.webassembly.js` are referenced
- **`App.razor`** is a pure Router component only — no HTML scaffolding
- **`Program.cs`** uses environment-aware API URL:
  ```csharp
  var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
      ? "https://localhost:{webapi-port}"   // must match WebApi launchSettings.json
      : builder.HostEnvironment.BaseAddress;
  ```
- **`_Imports.razor`** (root) — global usings applied to all pages
- **`_Imports.razor`** (feature folder) — feature-scoped service injections

### Feature Folder Structure

```
BankRecon.Bsui/
├── Features/
│   ├── Home.razor
│   └── YourFeature/
│       ├── _Imports.razor    ← @inject IYourFeatureService, @using Dtos
│       ├── Index.razor       ← list page
│       └── Detail.razor      ← detail page
├── Shared/
│   └── MainLayout.razor
├── wwwroot/
│   └── index.html
├── App.razor
├── _Imports.razor
└── Program.cs
```

### MudBlazor Component Notes (v7.12.0)

| Issue | Correct Usage |
|-------|--------------|
| `MudChip` requires generic type | Always use `<MudChip T="string">` |
| Column in `MudDataGrid` | Use `<PropertyColumn>` for bound props, `<TemplateColumn>` for custom content |
| `SkeletonType` | Only `Text`, `Circle`, `Rectangle` are valid |

---

## 📊 Audit & Soft Delete System

### Overview

- ✅ **Automatic Change Tracking** — All create, update, delete operations automatically captured
- ✅ **Soft Delete** — Deleted records marked as deleted, not permanently removed
- ✅ **Query Filters** — Soft-deleted entities automatically excluded
- ✅ **Audit Log Viewer** — UI pages available at `/auditlogs` and `/auditlogs/{id}`

---

## 🔍 Code Review Checklist

Before submitting a PR, ensure:

- [ ] Code follows `.editorconfig` rules
- [ ] Naming conventions are applied
- [ ] No hardcoded values (use configuration)
- [ ] Proper error handling (domain exceptions backend, `ApiResponse` handling in client)
- [ ] AutoMapper profiles implemented via `IMapFrom<T>`
- [ ] XML documentation on public methods
- [ ] Domain layer remains dependency-free
- [ ] Soft delete used for appropriate entities
- [ ] Client service interface in `Features/Interfaces/`
- [ ] Client service implementation in `Features/Services/`
- [ ] Feature services registered in `DependencyInjection.cs`
- [ ] Blazor feature has `_Imports.razor` with service injection
- [ ] `MudChip` uses `T="string"` type parameter
- [ ] `MudDataGrid` uses `PropertyColumn` / `TemplateColumn` (not `Column`)
- [ ] Responsive design tested on mobile/desktop
- [ ] Loading states and error handling implemented
- [ ] Snackbar notifications for user feedback

---

## 🚀 Git Workflow

1. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Commit with conventional format**
   ```bash
   git commit -m "feat: add your feature description"
   ```

**Types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

---

## 📚 Resources

- [Microsoft Learn - ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Clean Architecture - Robert Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [MudBlazor Components](https://mudblazor.com/)

---

## 🙏 Thank You

We appreciate your contributions to making BankRecon better!
