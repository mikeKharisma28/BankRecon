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
- [x] Proper error response formatting

#### Controllers & Endpoints
- [x] Base controller pattern
- [x] AuditLog endpoints (`GET /api/auditlogs`, `GET /api/auditlogs/{id}`)
- [x] Proper HTTP status codes and response wrapping
- [x] Query support for filtering/pagination

#### Swagger Documentation
- [x] API documentation
- [x] Request/response examples
- [x] Authentication scheme definition (placeholder)

### ✅ Phase 3: Blazor WebAssembly Client (COMPLETED)

#### Bsui.Client Layer
- [x] Base API client (`IApiClient`, `ApiClient`) with typed HTTP operations
- [x] Feature-specific services (`IAuditLogService`, `AuditLogService`)
- [x] Automatic response deserialization and error handling
- [x] Dependency injection setup (`AddBsuiClient()`)

#### HTTP Client Configuration
- [x] Typed `IHttpClientFactory` usage
- [x] Base address configuration
- [x] Authorization header support (ready for JWT)

### ✅ Phase 4: Bsui Foundation & Audit Log UI (COMPLETED)

#### Bsui Foundation
- [x] `index.html` with Bootstrap 5 CSS and Bootstrap Icons
- [x] `App.razor` with router configuration
- [x] `MainLayout.razor` with navbar and sidebar
- [x] `NavMenu.razor` with navigation links and proper icon alignment
- [x] `Program.cs` with service registration
- [x] `_Imports.razor` with global using statements

#### Audit Log Features
- [x] Audit Log list page (`Index.razor`) with refresh capability
- [x] Audit Log detail page (`Detail.razor`) with JSON formatting
- [x] Audit Log table component (`AuditLogsTable.razor`) as feature-specific wrapper

### 🚀 Phase 5: Reusable Components & Remaining UI Features (IN PROGRESS)

#### Reusable Components Library ✅ (Started)
- [x] **Generic DataTable Component** — Dynamic, sortable table for any data type
  - [x] Support for dynamic columns and data binding via `DataTableColumn<T>`
  - [x] Built-in action buttons customization with `RenderFragment<TItem>`
  - [x] Responsive design with Bootstrap 5 table classes
  - [x] Sorting capabilities with sort direction tracking
  - [x] Empty state handling
  - [x] Live usage in `Weather.razor` demo page
- [ ] **Generic Form Component** — Reusable form with validation
- [ ] **Modal Dialog Component** — For confirmations and dialogs
- [ ] **Notification Components** — Toast/alert system

#### Entity Management Pages
- [ ] Entity list pages with DataTable component
- [ ] Entity detail pages
- [ ] Create/Edit form pages with validation
- [ ] Delete confirmation dialogs

#### Advanced Features
- [ ] Client-side form validation with visual feedback
- [ ] Global state management (if needed)
- [ ] Error handling UI components
- [ ] Authentication and Authorization (JWT)
- [ ] Role-based access control (RBAC)
- [ ] Advanced search & filtering
- [ ] Bulk operations
- [ ] Export/Import functionality
- [ ] PerformedBy user identity population
- [ ] Unit & integration tests

## 🏗️ Project Structure

### Folder Organization

```
src/
├── BankRecon.Domain/                     # Domain layer
├── BankRecon.Application/                # Application layer
├── BankRecon.Infrastructure/             # Infrastructure layer
├── BankRecon.Shared/                     # Shared DTOs and models
├── BankRecon.WebApi/                     # Web API layer
├── BankRecon.Bsui.Client/                # Blazor client services
└── BankRecon.Bsui/                       # Blazor WebAssembly UI
    ├── Common/
    │   ├── Components/                   # Reusable components (DataTable, Form, Modal, etc.)
    │   │   └── DataTable/             
    │   │       ├── DataTable.razor
    │   │       └── DataTableColumn.cs
    │   └── Layout/                       # Layout components
    └── Features/
        ├── Home/
        ├── AuditLogs/
        │   ├── Index.razor
        │   ├── Detail.razor
        │   └── Components/
        │       └── AuditLogsTable.razor  # Feature-specific wrapper
        └── [Other Features]/
```

### Component Guidelines

#### Common Components (`Shared/Components/Common/`)

Reusable components must follow these guidelines:

1. **Generic Type Parameters** — Use `<TItem>` for data binding
2. **Flexible Columns** — Support dynamic column definitions via `DataTableColumn<TItem>`
3. **Action Callbacks** — Expose `RenderFragment<TItem>` for custom actions
4. **Bootstrap 5 Only** — No external UI libraries (MudBlazor, etc.)
5. **Naming Convention** — PascalCase for component names (e.g., `DataTable.razor`)
6. **Parameter Documentation** — Add `/// <summary>` comments for parameters

#### Example: Using DataTable Component

```razor
@page "/myentities"
@using BankRecon.Bsui.Shared.Components.Common
@using MyNamespace.Features.MyEntities

<h2>My Entities</h2>

<DataTable TItem="MyEntityDto" 
           Items="@entities" 
           Columns="@columns" 
           Actions="@RenderActions" 
           OnSortChange="@HandleSort" />

@code {
    private List<MyEntityDto> entities = new();
    private List<DataTableColumn<MyEntityDto>> columns = new();

    protected override void OnInitialized()
    {
        columns = new()
        {
            new()
            {
                Header = "ID",
                PropertyName = "Id",
                Property = x => x.Id,
                IsSortable = true
            },
            new()
            {
                Header = "Name",
                PropertyName = "Name",
                Property = x => x.Name,
                IsSortable = true
            },
            new()
            {
                Header = "Created",
                PropertyName = "CreatedAt",
                Property = x => x.CreatedAt,
                IsSortable = true,
                CellTemplate = item => (RenderFragment)(builder =>
                {
                    builder.AddContent(0, item.CreatedAt.ToString("g"));
                })
            },
        };
    }

    private RenderFragment<MyEntityDto> RenderActions => item => (RenderFragment)(builder =>
    {
        builder.OpenElement(0, "button");
        builder.AddAttribute(1, "class", "btn btn-sm btn-outline-primary");
        builder.AddAttribute(2, "@onclick", EventCallback.Factory.Create(this, () => ViewDetails(item.Id)));
        builder.AddContent(3, "View");
        builder.CloseElement();
    });

    private void ViewDetails(Guid id) => Navigation.NavigateTo($"/myentities/{id}");
    
    private Task HandleSort((string PropertyName, bool IsDescending) sort)
    {
        // Implement sorting logic
        return Task.CompletedTask;
    }
}
```

#### Creating Feature-Specific Wrappers

For features that need customization, create a wrapper component in `Features/YourFeature/Components/`:

```razor
@* Features/MyEntities/Components/MyEntitiesTable.razor *@
@using BankRecon.Shared.Features.MyEntities.Dtos
@using BankRecon.Bsui.Shared.Components.Common

<DataTable TItem="MyEntityDto" Items="@MyEntities" Columns="@columns" Actions="@RenderActions" />

@code {
    [Parameter]
    public List<MyEntityDto> MyEntities { get; set; } = new();

    [Parameter]
    public EventCallback<Guid> OnViewDetails { get; set; }

    private List<DataTableColumn<MyEntityDto>> columns = new();

    protected override void OnInitialized()
    {
        // Define columns with custom templates, styling, etc.
        columns = new()
        {
            new() { Header = "Name", PropertyName = "Name", Property = x => x.Name },
            // ... more columns
        };
    }

    private RenderFragment<MyEntityDto> RenderActions => item => (RenderFragment)(builder =>
    {
        // Custom action buttons
    });
}
```

## 🔧 Coding Standards

### Blazor Components

- **File scoped namespaces** — Use `namespace BankRecon.Bsui.Shared.Components.Common;`
- **Bootstrap 5 only** — No MudBlazor or other UI libraries
- **Parameter naming** — Use clear, descriptive names (e.g., `OnViewDetails`, `Items`)
- **Event naming** — Prefix with `On` (e.g., `OnActionClicked`, `OnSortChange`)
- **CSS** — Use Bootstrap classes; inline styles only for component-specific styling
- **Responsiveness** — Ensure mobile-friendly layouts using Bootstrap grid system
- **Documentation** — Add `/// <summary>` comments to public parameters and methods

### Example Component Structure

```razor
@namespace BankRecon.Bsui.Shared.Components.Common
@typeparam TItem

<div class="table-responsive">
    <table class="table table-striped table-hover">
        <thead class="table-dark">
            <!-- Headers -->
        </thead>
        <tbody>
            <!-- Body -->
        </tbody>
    </table>
</div>

@code {
    /// <summary>
    /// The collection of items to display.
    /// </summary>
    [Parameter]
    public List<TItem> Items { get; set; } = new();

    // ... more parameters
}
```

## 🔐 Code Standards

This project enforces strict code standards via `.editorconfig`:

- **Indentation:** 4 spaces
- **Line endings:** CRLF (Windows)
- **Character encoding:** UTF-8
- **Naming conventions:** PascalCase (types), camelCase (locals)
- **Namespaces:** File-scoped
- **Null safety:** Nullable reference types enabled

For detailed style rules, see the root `.editorconfig` file.

## 📖 Learning Resources

- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://www.domainlanguage.com/ddd/)
- [MediatR - CQRS Pattern](https://github.com/jbogard/MediatR)
- [Entity Framework Core Docs](https://docs.microsoft.com/en-us/ef/core/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/)
- [Bootstrap Icons](https://icons.getbootstrap.com/)

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

## 🤝 Contributing Workflow

### Creating a New Feature

1. **Define the domain entity** (in `BankRecon.Domain`)
2. **Create entity configuration** (in `BankRecon.Infrastructure`)
3. **Create DTOs and validators** (in `BankRecon.Application`)
4. **Create MediatR handlers** (Commands/Queries)
5. **Create API controller** (in `BankRecon.WebApi`)
6. **Create client service** (in `BankRecon.Bsui.Client`)
7. **Create Blazor pages** (in `BankRecon.Bsui/Features`)
   - Use `DataTable` component for list pages
   - Create feature-specific wrapper if customization needed

### Using Reusable Components

When building new features, **always leverage common components** to reduce code duplication:

✅ **DO:**
```razor
<DataTable TItem="MyEntityDto" 
           Items="@entities" 
           Columns="@columns" 
           Actions="@RenderActions" />
```

❌ **DON'T:**
```razor
<table class="table">
    <thead>
        <!-- Manual table markup -->
    </thead>
</table>
```

### Before Committing

1. Ensure code follows `.editorconfig` rules
2. Test the feature end-to-end
3. Verify responsive design on mobile devices
4. Update README.md and CONTRIBUTING.md if adding new components or phases
5. Update the implementation status checklist

## 📄 License

This project is licensed under the MIT License — see the LICENSE file for details.

## 👤 Author

**Michael Laksa Kharisma** — [@mikeKharisma28](https://github.com/mikeKharisma28)

## 📞 Support

For issues, questions, or suggestions, please open an [issue](https://github.com/mikeKharisma28/BankRecon/issues) on GitHub.

---

**Status:** 🚧 Under Development | **Current Phase:** Phase 5 — Reusable Components & Remaining UI Features In Progress | **Last Updated:** April 2026
