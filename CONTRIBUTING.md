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
- [x] Pipeline behaviors (ValidationBehavior, LoggingBehavior)
- [x] DependencyInjection registration (`AddApplication()`)
- [x] Audit Log queries:
  - [x] GetAllAuditLogsQuery
  - [x] GetAuditLogsByEntityQuery

#### Infrastructure Layer
- [x] DbContext (`BankReconDbContext`)
- [x] Generic Repository<T> implementation
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
- [x] CORS setup for Blazor WebAssembly
- [x] Exception handling middleware (`ExceptionHandlingMiddleware`)
- [x] Pipeline configuration (middleware ordering)

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
- [x] Base API client interface (`IApiClient`)
- [x] Base API client implementation (`ApiClient`)
- [x] Typed HttpClient with `IHttpClientFactory` via `AddHttpClient<IApiClient, ApiClient>()`
- [x] Feature-specific service interfaces and implementations
  - [x] `IAuditLogService` / `AuditLogService`
- [x] DependencyInjection extension (`AddBsuiClient()`)
- [x] Proper namespacing following project patterns

#### Blazor WebAssembly Project
- [x] Blazor WebAssembly project scaffolded (`BankRecon.Bsui`)
- [x] MudBlazor integrated (v7.12.0) — CSS, JS, `AddMudServices()`
- [x] `Program.cs` configured (Bsui.Client DI registration, `AddMudServices()`, root components)
- [x] `App.razor`, `Routes.razor`, `_Import.razor` configured with Bsui.Client namespaces
- [x] `MainLayout.razor` with MudBlazor layout (`MudThemeProvider`, `MudDialogProvider`, `MudSnackbarProvider`)
- [x] `Features/` folder scaffolded for feature organization

### 📍 Phase 4: Advanced Features & UI Pages (IN PROGRESS)

#### Blazor Feature Pages
- [ ] Feature pages for entity management (list, detail, create/edit)
- [ ] Audit log viewer page
- [ ] Client-side form validation
- [ ] Global state management / component communication
- [ ] Error handling UI components
- [ ] Loading indicators and spinners
- [ ] Navigation menu with routing

#### Advanced Features (PLANNED)
- [ ] Authentication and Authorization (JWT)
- [ ] Role-based access control (RBAC)
- [ ] Advanced search & filtering
- [ ] Bulk operations
- [ ] Export/Import functionality
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

3. **Implement IMapFrom<T>** in DTO
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

5. **Create Handler**
   ```csharp
   public class YourCommandHandler : IRequestHandler<YourCommand, ApiResponse<YourDto>>
   {
       public async Task<ApiResponse<YourDto>> Handle(...)
       {
           // Implementation
       }
   }
   ```

6. **Create Validator** (for Commands)
   ```csharp
   public class YourCommandValidator : AbstractValidator<YourCommand>
   {
       public YourCommandValidator()
       {
           RuleFor(x => x.Property).NotEmpty();
       }
   }
   ```

7. **Create Controller Endpoint** (if public API)
   ```
   src/BankRecon.WebApi/Controllers/YourController.cs
   ```

8. **Create Client Service** (for Blazor consumption)
   ```
   src/BankRecon.Bsui.Client/Features/YourFeature/
   ├── IYourFeatureService.cs
   └── YourFeatureService.cs
   ```

   Register in `src/BankRecon.Bsui.Client/DependencyInjection.cs`:
   ```csharp
   services.AddScoped<IYourFeatureService, YourFeatureService>();
   ```

9. **Create Blazor Pages** (for UI)
   ```
   src/BankRecon.Bsui/Features/YourFeature/
   ├── Index.razor
   ├── Detail.razor
   └── Index.razor.cs (code-behind, optional)
   ```

### Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| **Command** | `{Action}{EntityName}Command` | `CreateTransactionCommand` |
| **Query** | `Get{EntityName}{Criteria}Query` | `GetAllTransactionsQuery`, `GetTransactionByIdQuery` |
| **Handler** | `{Command/Query}Handler` | `CreateTransactionCommandHandler` |
| **Validator** | `{Command}Validator` | `CreateTransactionValidator` |
| **DTO** | `{EntityName}Dto` | `TransactionDto` |
| **Entity** | `{EntityName}` | `Transaction` |
| **Repository** | `I{EntityName}Repository` | `ITransactionRepository` |
| **Service** | `I{FeatureName}Service` | `ITransactionService` |

---

## 📊 Audit & Soft Delete System

### Overview

The application implements a **comprehensive audit trail system** with **soft delete capability**:

- ✅ **Automatic Change Tracking** — All create, update, delete operations are automatically captured
- ✅ **Soft Delete** — Deleted records are marked as deleted, not permanently removed
- ✅ **Query Filters** — Soft-deleted entities automatically excluded from queries
- ✅ **Audit Log Entity** — `AuditLog` stores: action type, old/new values, affected columns, timestamp, user
- ✅ **Zero Application Code** — Audit capturing happens transparently via `DbContext.SaveChangesAsync()` override
- ✅ **Audit Log Queries** — Specialized repository with rich query methods

### How It Works

#### 1. Soft Delete Mechanism

When deleting a `SoftDeletableEntity`:
- Data is **not** permanently removed from the database
- `IsDeleted = true`, `DeletedAt = DateTimeOffset.UtcNow`, `DeletedBy = {user}` are set
- Global query filter automatically excludes deleted records: `!IsDeleted`
- Use `IgnoreQueryFilters()` to retrieve soft-deleted records

**Example:**
```csharp
// Delete (soft)
await repository.DeleteAsync(entityId);  // Marks as deleted

// Restore
await repository.RestoreAsync(entityId); // Restores the record

// Get including deleted
var allRecords = await repository.GetAllIncludingDeletedAsync();
```

#### 2. Audit Log Capturing

Every operation (Create, Update, Delete) is automatically logged in the `AuditLog` table:

**For Create:**
```json
{
  "EntityName": "Transaction",
  "EntityId": "550e8400-e29b-41d4-a716-446655440000",
  "Action": "Create",
  "NewValues": { "Id": "550e8400...", "Description": "Test", "Amount": 100.00, "CreatedAt": "2026-04-04T10:00:00Z" },
  "OldValues": null,
  "AffectedColumns": null,
  "Timestamp": "2026-04-04T10:00:00Z",
  "PerformedBy": null  // Populated if user identity is available
}
```

**For Update:**
```json
{
  "EntityName": "Transaction",
  "EntityId": "550e8400-e29b-41d4-a716-446655440000",
  "Action": "Update",
  "OldValues": { "Description": "Test", "Amount": 100.00 },
  "NewValues": { "Description": "Updated Test", "Amount": 150.00 },
  "AffectedColumns": "Description, Amount",
  "Timestamp": "2026-04-04T10:15:00Z",
  "PerformedBy": null  // Populated if user identity is available
}
```

**For Delete (Soft):**
```json
{
  "EntityName": "Transaction",
  "EntityId": "550e8400-e29b-41d4-a716-446655440000",
  "Action": "Delete",
  "OldValues": { "Description": "Updated Test", "Amount": 150.00, "DeletedAt": "2026-04-04T10:20:00Z", "IsDeleted": true },
  "NewValues": null,
  "AffectedColumns": null,
  "Timestamp": "2026-04-04T10:20:00Z",
  "PerformedBy": null  // Populated if user identity is available
}
```

#### 3. Audit Field Tracking

**Create Operations:**
- `CreatedAt` — Automatically set to `DateTimeOffset.UtcNow`
- `CreatedBy` — Optional; set by application code or user identity (see enhancement below)

**Update Operations:**
- `UpdatedAt` — Automatically set to `DateTimeOffset.UtcNow`
- `UpdatedBy` — Optional; set by application code or user identity (see enhancement below)

**Delete Operations (Soft):**
- `DeletedAt` — Automatically set to `DateTimeOffset.UtcNow`
- `DeletedBy` — Optional; set by application code or user identity (see enhancement below)

#### 4. Querying Audit Logs

Use the `IAuditLogRepository` to query audit trails. It provides specialized methods beyond basic CRUD:

```csharp
// Get all audit logs
var allLogs = await _auditLogRepository.GetAllAsync(cancellationToken);

// Get logs for a specific entity
var entityLogs = await _auditLogRepository.GetByEntityAsync("Transaction", entityId, cancellationToken);

// Get logs for all instances of an entity type
var allTransactionLogs = await _auditLogRepository.GetByEntityNameAsync("Transaction", cancellationToken);

// Get logs within a date range
var logsInRange = await _auditLogRepository.GetByDateRangeAsync(startDate, endDate, cancellationToken);

// Get logs by action type (Create, Update, Delete)
var createdLogs = await _auditLogRepository.GetByActionAsync("Create", cancellationToken);
```

#### 5. Enhancement: Capture `PerformedBy` & User Identity

To automatically populate `CreatedBy`, `UpdatedBy`, `DeletedBy`, and `PerformedBy`, inject `IHttpContextAccessor`:

```csharp
// In BankReconDbContext
private readonly IHttpContextAccessor _httpContextAccessor;

public BankReconDbContext(
    DbContextOptions<BankReconDbContext> options,
    IHttpContextAccessor httpContextAccessor)
    : base(options)
{
    _httpContextAccessor = httpContextAccessor;
}

private string? GetCurrentUser()
{
    return _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
}

// Then in OnBeforeSaveChanges(), use GetCurrentUser() to populate audit entry user
```

---

## 🌐 Bsui.Client Layer

### Overview

The `BankRecon.Bsui.Client` layer serves as the **API client infrastructure** for the Blazor WebAssembly frontend. It abstracts HTTP communication and provides type-safe services for consuming WebAPI endpoints.

### Architecture

**Responsibility:** Bridge between Blazor components and the WebAPI backend via HTTP

**Dependency Flow:**
```
BankRecon.Bsui (Blazor Components)
    ↓ injects
BankRecon.Bsui.Client (API Services)
    ↓ calls
BankRecon.WebApi (REST Endpoints)
```

### Layer Structure

```
BankRecon.Bsui.Client/
├── Common/
│   ├── Interfaces/
│   │   └── IApiClient.cs                # Base HTTP client contract
│   └── Services/
│       └── ApiClient.cs                 # Base HTTP client implementation
├── Features/
│   ├── AuditLogs/
│   │   ├── IAuditLogService.cs          # Feature-specific interface
│   │   └── AuditLogService.cs           # Feature-specific implementation
│   └── YourFeature/
│       ├── IYourFeatureService.cs
│       └── YourFeatureService.cs
├── BankRecon.Bsui.Client.csproj
└── DependencyInjection.cs
```

### Key Patterns

#### 1. Base API Client (IApiClient)

Provides a reusable abstraction for all HTTP operations:

```csharp
public interface IApiClient
{
    Task<ApiResponse<T>> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default);
    Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<T>> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default);
}
```

**Benefits:**
- ✅ Centralized error handling
- ✅ Automatic `ApiResponse<T>` deserialization
- ✅ Consistent HTTP handling across all services
- ✅ Easy to mock in unit tests

#### 2. Feature-Specific Services

Each feature provides a typed service matching the WebAPI controllers:

```csharp
public interface IAuditLogService
{
    Task<ApiResponse<List<AuditLogDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<List<AuditLogDto>>> GetByEntityAsync(string entityName, string entityId, CancellationToken cancellationToken = default);
}
```

**Usage in Blazor Components:**

```razor
@inject IAuditLogService AuditLogService

@code {
    private List<AuditLogDto> auditLogs = new();
    private bool isLoading = false;

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;
        var response = await AuditLogService.GetAllAsync();
        if (response.IsSuccess && response.Result != null)
        {
            auditLogs = response.Result;
        }
        isLoading = false;
    }
}
```

#### 3. Typed HttpClient Registration

Uses `IHttpClientFactory` for automatic instance reuse and lifecycle management:

```csharp
// In DependencyInjection.cs
public static IServiceCollection AddBsuiClient(
    this IServiceCollection services,
    string baseAddress)
{
    services.AddHttpClient<IApiClient, ApiClient>(client =>
    {
        client.BaseAddress = new Uri(baseAddress);
    });

    services.AddScoped<IAuditLogService, AuditLogService>();

    return services;
}
```

Called in `BankRecon.Bsui/Program.cs`:

```csharp
builder.Services.AddBsuiClient(builder.HostEnvironment.BaseAddress);
```

### Guidelines for New Services

When adding a new feature service:

1. **Create the interface** in `Features/{FeatureName}/I{FeatureName}Service.cs`
   ```csharp
   public interface I{FeatureName}Service
   {
       Task<ApiResponse<...>> Get...Async(...);
       Task<ApiResponse<...>> Create...Async(...);
       // etc.
   }
   ```

2. **Create the implementation** in `Features/{FeatureName}/{FeatureName}Service.cs`
   ```csharp
   public class {FeatureName}Service : I{FeatureName}Service
   {
       private readonly IApiClient _apiClient;

       public {FeatureName}Service(IApiClient apiClient)
       {
           _apiClient = apiClient;
       }

       public async Task<ApiResponse<...>> Get...Async(...)
       {
           return await _apiClient.GetAsync<...>("api/{endpoint}", cancellationToken);
       }
   }
   ```

3. **Register in DependencyInjection.cs**
   ```csharp
   services.AddScoped<I{FeatureName}Service, {FeatureName}Service>();
   ```

4. **Add using statement in `_Import.razor`**
   ```razor
   @using BankRecon.Bsui.Client.Features.{FeatureName}
   ```

### Dependencies

- **Minimal:** References only `BankRecon.Shared` (for DTOs and `ApiResponse<T>`)
- **No backend dependencies:** Does not reference `BankRecon.Application`, `BankRecon.Infrastructure`, or `BankRecon.Domain`
- **NuGet packages:** `Microsoft.Extensions.Http` (for typed HttpClient support)

---

## 🔍 Code Review Checklist

Before submitting a PR, ensure:

- [ ] Code follows `.editorconfig` rules
- [ ] Naming conventions are applied
- [ ] No hardcoded values (use configuration)
- [ ] Proper error handling (throw domain exceptions in backend, handle `ApiResponse` in client)
- [ ] Input validation in validators (backend) and form validation (Blazor)
- [ ] AutoMapper profiles implemented (via `IMapFrom<T>`)
- [ ] XML documentation added to public methods
- [ ] No direct repository access from UI (use MediatR in backend, services in client)
- [ ] Domain layer remains dependency-free
- [ ] Soft delete is used for appropriate entities
- [ ] Audit logs are captured for all operations
- [ ] Specialized repositories used for non-BaseEntity entities
- [ ] Client services properly abstracted behind interfaces
- [ ] Feature services registered in `DependencyInjection.cs`
- [ ] Tests pass (if added)

---

## 🔐 Dependency Injection Rules

### ✅ Correct Pattern (Backend)

```csharp
// In handler or service
private readonly IRepository<Entity> _repository;

public MyHandler(IRepository<Entity> repository)
{
    _repository = repository;
}
```

### ✅ Correct Pattern (Client)

```csharp
// In service
private readonly IApiClient _apiClient;

public MyService(IApiClient apiClient)
{
    _apiClient = apiClient;
}
```

### ❌ Anti-patterns

```csharp
// DON'T: Static access
var result = EntityRepository.GetAll();

// DON'T: Service locator
var repo = ServiceLocator.GetService<IRepository>();

// DON'T: Direct instantiation
var repo = new Repository(dbContext);
var httpClient = new HttpClient();
```

---

## 📁 Project File Structure Rules

### Domain Layer
```
BankRecon.Domain/
├── Common/
│   ├── BaseEntity.cs
│   ├── SoftDeletableEntity.cs
│   └── Interfaces/
│       ├── IHasKey.cs
│       ├── ICreatable.cs
│       ├── IUpdatable.cs
│       └── ISoftDeletable.cs
└── Entities/
    ├── AuditLog.cs
    └── YourEntity.cs
```

### Application Layer
```
BankRecon.Application/
├── Common/
│   ├── Behaviors/
│   ├── Exceptions/
│   ├── Interfaces/
│   │   ├── IRepository.cs
│   │   └── IAuditLogRepository.cs
│   └── Mappings/
├── Features/
│   ├── AuditLogs/
│   │   └── Queries/
│   │       ├── GetAllAuditLogs/
│   │       └── GetAuditLogsByEntity/
│   └── YourFeature/
│       ├── Commands/
│       ├── Queries/
│       └── Validators/
└── DependencyInjection.cs
```

### Infrastructure Layer
```
BankRecon.Infrastructure/
├── Data/
│   └── BankReconDbContext.cs
├── Repositories/
│   ├── Repository.cs
│   └── AuditLogRepository.cs
├── Configurations/
│   ├── BaseEntityConfiguration.cs
│   ├── SoftDeletableEntityConfiguration.cs
│   ├── AuditLogConfiguration.cs
│   └── YourEntityConfiguration.cs
└── DependencyInjection.cs
```

### Shared Layer
```
BankRecon.Shared/
├── Common/
│   ├── Responses/
│   ├── Models/
│   └── Mappings/
└── Features/
    ├── AuditLogs/
    │   └── Dtos/
    │       └── AuditLogDto.cs
    └── YourFeature/
        └── Dtos/
            └── YourDto.cs
```

### Bsui.Client Layer
```
BankRecon.Bsui.Client/
├── Common/
│   ├── Interfaces/
│   │   └── IApiClient.cs
│   └── Services/
│       └── ApiClient.cs
├── Features/
│   ├── AuditLogs/
│   │   ├── IAuditLogService.cs
│   │   └── AuditLogService.cs
│   └── YourFeature/
│       ├── IYourFeatureService.cs
│       └── YourFeatureService.cs
├── BankRecon.Bsui.Client.csproj
└── DependencyInjection.cs
```

### Blazor UI Layer
```
BankRecon.Bsui/
├── Pages/
│   └── YourFeature/
│       ├── Index.razor
│       └── Detail.razor
├── Shared/
│   ├── MainLayout.razor
│   ├── NavMenu.razor
│   └── Components/
├── Features/
├── App.razor
├── Routes.razor
├── _Import.razor
└── Program.cs
```

---

## 🚀 Git Workflow

1. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make changes and commit**
   ```bash
   git add .
   git commit -m "feat: add your feature description"
   ```

3. **Push to remote**
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Create Pull Request**
   - Link related issues
   - Describe changes clearly
   - Request review from maintainers

### Commit Message Format

```
<type>: <subject>

<body>

<footer>
```

**Types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

**Example:**
```
feat: add audit log service to Bsui.Client layer

- Create IAuditLogService interface
- Implement AuditLogService with GetAll and GetByEntity methods
- Register service in DependencyInjection
- Add feature usings to _Import.razor

Closes #123
```

---

## ✅ Quality Standards

### Required

- Code must compile without warnings
- All public APIs must have XML documentation
- Entity configurations must use Fluent API
- No raw queries (use Repository pattern)
- Domain layer must be dependency-free
- All soft-deletable entities must use `SoftDeletableEntity`
- Audit logs must be captured for all operations
- Non-BaseEntity entities must use specialized repositories
- Client services must be properly abstracted behind interfaces
- Feature services must be registered in DI

### Recommended

- Unit tests for business logic
- Integration tests for handlers and services
- Performance tests for queries
- Error scenario testing
- Documentation for complex features
- Component tests for Blazor pages

---

## 🐛 Reporting Issues

When reporting bugs, please include:

1. **Description** — What doesn't work?
2. **Steps to reproduce** — How to trigger the bug
3. **Expected behavior** — What should happen?
4. **Actual behavior** — What actually happens?
5. **Environment** — OS, .NET version, Visual Studio version
6. **Logs** — Error messages or stack traces

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

## 📞 Questions?

Open a discussion or issue on GitHub for questions about the project architecture or development process.

---

## 🙏 Thank You

We appreciate your contributions to making BankRecon better!
