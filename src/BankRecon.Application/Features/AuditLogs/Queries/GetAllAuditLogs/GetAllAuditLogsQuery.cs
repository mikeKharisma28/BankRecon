using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;
using MediatR;

namespace BankRecon.Application.Features.AuditLogs.Queries.GetAllAuditLogs;

/// <summary>
/// Query to retrieve all audit log entries.
/// </summary>
public record GetAllAuditLogsQuery : IRequest<ApiResponse<List<AuditLogDto>>>;
