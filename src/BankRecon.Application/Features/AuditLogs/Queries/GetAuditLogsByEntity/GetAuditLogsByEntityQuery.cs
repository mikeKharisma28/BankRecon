using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;
using MediatR;

namespace BankRecon.Application.Features.AuditLogs.Queries.GetAuditLogsByEntity;

/// <summary>
/// Query to retrieve audit logs for a specific entity.
/// </summary>
public record GetAuditLogsByEntityQuery(
    string EntityName,
    string EntityId) : IRequest<ApiResponse<List<AuditLogDto>>>;
