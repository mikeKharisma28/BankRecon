# Contributing to BankRecon

Thank you for your interest in contributing to BankRecon! This document outlines the project structure, development guidelines, and contribution workflow.

## 📋 Development Phases & Status

### ✅ Phase 1: Core Architecture & Application Layer (COMPLETED)

#### Domain Layer
- [x] Base entities (`BaseEntity`, `SoftDeletableEntity`)
- [x] Domain interfaces (`IHasKey`, `ICreatable`, `IUpdatable`, `ISoftDeletable`)
- [x] Example entity (`ExampleSoftDeletableEntity`)
- [x] Removed external dependencies (Domain layer is clean)

#### Shared Layer
- [x] API response wrapper (`ApiResponse<T>`)
- [x] Pagination model (`PaginatedList<T>`)
- [x] Mapping interface (`IMapFrom<T>`)
- [x] DTOs for Example entity
- [x] Minimized dependencies (only AutoMapper + Domain)

#### Application Layer
- [x] Domain exceptions (`EntityNotFoundException`, `ValidationException`)
- [x] MediatR CQRS setup with command/query handlers
- [x] FluentValidation validators
- [x] AutoMapper configuration with reflection-based discovery
- [x] Pipeline behaviors (ValidationBehavior, LoggingBehavior)
- [x] DependencyInjection registration (`AddApplication()`)
- [x] Example CRUD operations:
  - [x] GetAllExampleSoftDeletableEntitiesQuery
  - [x] GetByIdExampleSoftDeletableEntityQuery
  - [x] CreateExampleSoftDeletableEntityCommand
  - [x] UpdateExampleSoftDeletableEntityCommand
  - [x] DeleteExampleSoftDeletableEntityCommand

#### Infrastructure Layer
- [x] DbContext (`BankReconDbContext`)
- [x] Generic Repository<T> implementation
- [x] Entity configurations (Fluent API)
- [x] DependencyInjection registration (`AddInfrastructure()`)
- [x] Soft delete query filtering

### ✅ Phase 2: WebApi Layer (COMPLETED)

#### Program.cs & Middleware
- [x] Dependency injection registration (Application + Infrastructure)
- [x] Swagger/OpenAPI configuration
- [x] CORS setup for Blazor WebAssembly
- [x] Exception handling middleware (`ExceptionHandlingMiddleware`)
- [x] Pipeline configuration (middleware ordering)

#### Controllers
- [x] Example controller (`ExampleSoftDeletableEntitiesController`)
- [x] CRUD endpoint implementations
- [x] Proper HTTP status codes
- [x] XML documentation for Swagger

#### Configuration
- [x] `appsettings.json` (base configuration)
- [x] `appsettings.Development.json` (development overrides)
- [x] Connection string setup
- [x] Logging configuration

### 🔄 Phase 3: Blazor WebAssembly Client (IN PROGRESS)

#### Project Setup
- [x] Blazor WebAssembly project scaffolded (`BankRecon.Bsui`)
- [x] MudBlazor integrated (v7.12.0) — CSS, JS, `AddMudServices()`
- [x] `Program.cs` configured (`HttpClient`, `AddMudServices()`, root components)
- [x] `App.razor`, `Routes.razor`, `_Import.razor` configured
- [x] `MainLayout.razor` with MudBlazor layout (`MudThemeProvider`, `MudDialogProvider`, `MudSnackbarProvider`)
- [x] `Features/` folder scaffolded

#### Pending
- [ ] HTTP client service / typed API client
- [ ] Feature pages for entity management (list, detail, create/edit)
- [ ] Client-side form validation
- [ ] State management
- [ ] Error handling UI
- [ ] Loading indicators
- [ ] Navigation menu

### 📍 Phase 4: Advanced Features (PLANNED)

- [ ] Authentication/Authorization (JWT)
- [ ] Role-based access control (RBAC)
- [ ] Advanced search & filtering
- [ ] Bulk operations
- [ ] Export/Import functionality
- [ ] Audit log viewer
- [ ] Unit & integration tests

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
   ├── Handlers/
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

### Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| **Command** | `{Action}{EntityName}Command` | `CreateTransactionCommand` |
| **Query** | `Get{EntityName}{Criteria}Query` | `GetAllTransactionsQuery`, `GetTransactionByIdQuery` |
| **Handler** | `{Command/Query}Handler` | `CreateTransactionCommandHandler` |
| **Validator** | `{Command}Validator` | `CreateTransactionValidator` |
| **DTO** | `{EntityName}Dto` | `TransactionDto` |
| **Entity** | `{EntityName}` | `Transaction` |

---

## 🔍 Code Review Checklist

Before submitting a PR, ensure:

- [ ] Code follows `.editorconfig` rules
- [ ] Naming conventions are applied
- [ ] No hardcoded values (use configuration)
- [ ] Proper error handling (throw domain exceptions)
- [ ] Input validation in validators
- [ ] AutoMapper profiles implemented (via `IMapFrom<T>`)
- [ ] XML documentation added to public methods
- [ ] No direct repository access from UI (use MediatR)
- [ ] Domain layer remains dependency-free
- [ ] Tests pass (if added)

---

## 🔐 Dependency Injection Rules

### ✅ Correct Pattern

```csharp
// In handler or service
private readonly IRepository<Entity> _repository;

public MyHandler(IRepository<Entity> repository)
{
    _repository = repository;
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
    └── YourEntity.cs
```

### Application Layer
```
BankRecon.Application/
├── Common/
│   ├── Behaviors/
│   ├── Exceptions/
│   ├── Interfaces/
│   └── Mappings/
├── Features/
│   └── YourFeature/
│       ├── Commands/
│       │   └── Create/YourCommand.cs
│       ├── Queries/
│       │   └── GetAll/YourQuery.cs
│       ├── Handlers/
│       ├── Validators/
│       └── YourFeatureProfile.cs (if complex mapping)
└── DependencyInjection.cs
```

### Infrastructure Layer
```
BankRecon.Infrastructure/
├── Data/
│   └── BankReconDbContext.cs
├── Repositories/
│   └── Repository.cs
├── EntityConfigurations/
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
    └── YourFeature/
        └── Dtos/
            └── YourDto.cs
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
feat: add soft delete support to ExampleEntity

- Implement ISoftDeletable interface
- Add DeletedAt and DeletedBy properties
- Update repository to filter deleted items

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

### Recommended

- Unit tests for business logic
- Integration tests for handlers
- Performance tests for queries
- Error scenario testing

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

---

## 📞 Questions?

Open a discussion or issue on GitHub for questions about the project architecture or development process.

---

## 🙏 Thank You

We appreciate your contributions to making BankRecon better!
