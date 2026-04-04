using BankRecon.Domain.Entities;

namespace BankRecon.Application.Common.Interfaces;

/// <summary>
/// Repository interface for audit log queries.
/// Provides specialized methods for retrieving audit trail data.
/// </summary>
public interface IAuditLogRepository
{
    /// <summary>
    /// Retrieves all audit log entries.
    /// </summary>
    Task<List<AuditLog>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit logs for a specific entity by name and ID.
    /// </summary>
    Task<List<AuditLog>> GetByEntityAsync(
        string entityName,
        string entityId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit logs for a specific entity name.
    /// </summary>
    Task<List<AuditLog>> GetByEntityNameAsync(
        string entityName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit logs within a specific date range.
    /// </summary>
    Task<List<AuditLog>> GetByDateRangeAsync(
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit logs by action type (Create, Update, Delete).
    /// </summary>
    Task<List<AuditLog>> GetByActionAsync(
        string action,
        CancellationToken cancellationToken = default);
}
