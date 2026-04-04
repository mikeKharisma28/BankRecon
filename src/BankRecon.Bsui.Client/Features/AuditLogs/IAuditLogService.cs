using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;

namespace BankRecon.Bsui.Client.Features.AuditLogs;

/// <summary>
/// Client-side service interface for audit log operations.
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    /// Retrieves all audit log entries.
    /// </summary>
    Task<ApiResponse<List<AuditLogDto>>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit logs for a specific entity.
    /// </summary>
    Task<ApiResponse<List<AuditLogDto>>> GetByEntityAsync(
        string entityName,
        string entityId,
        CancellationToken cancellationToken = default);
}
