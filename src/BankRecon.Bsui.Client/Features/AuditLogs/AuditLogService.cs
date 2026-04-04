using BankRecon.Bsui.Client.Common.Interfaces;
using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;

namespace BankRecon.Bsui.Client.Features.AuditLogs;

/// <summary>
/// Client-side service for audit log API operations.
/// </summary>
public class AuditLogService : IAuditLogService
{
    private readonly IApiClient _apiClient;

    public AuditLogService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<List<AuditLogDto>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _apiClient.GetAsync<List<AuditLogDto>>(
            "api/AuditLogs", cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ApiResponse<List<AuditLogDto>>> GetByEntityAsync(
        string entityName,
        string entityId,
        CancellationToken cancellationToken = default)
    {
        return await _apiClient.GetAsync<List<AuditLogDto>>(
            $"api/AuditLogs/entity/{entityName}/{entityId}", cancellationToken);
    }
}
