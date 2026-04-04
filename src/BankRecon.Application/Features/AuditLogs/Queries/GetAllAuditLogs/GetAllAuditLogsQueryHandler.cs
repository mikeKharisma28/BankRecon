using AutoMapper;
using BankRecon.Application.Common.Interfaces;
using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;
using MediatR;

namespace BankRecon.Application.Features.AuditLogs.Queries.GetAllAuditLogs;

/// <summary>
/// Handler for retrieving all audit log entries.
/// </summary>
public class GetAllAuditLogsQueryHandler
    : IRequestHandler<GetAllAuditLogsQuery, ApiResponse<List<AuditLogDto>>>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IMapper _mapper;

    public GetAllAuditLogsQueryHandler(IAuditLogRepository auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<AuditLogDto>>> Handle(
        GetAllAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        var auditLogs = await _auditLogRepository.GetAllAsync(cancellationToken);
        var dtos = _mapper.Map<List<AuditLogDto>>(auditLogs);
        return ApiResponse<List<AuditLogDto>>.Success(dtos);
    }
}
