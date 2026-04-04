using AutoMapper;
using BankRecon.Application.Common.Interfaces;
using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;
using MediatR;

namespace BankRecon.Application.Features.AuditLogs.Queries.GetAuditLogsByEntity;

/// <summary>
/// Handler for retrieving audit logs for a specific entity.
/// </summary>
public class GetAuditLogsByEntityQueryHandler
    : IRequestHandler<GetAuditLogsByEntityQuery, ApiResponse<List<AuditLogDto>>>
{
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IMapper _mapper;

    public GetAuditLogsByEntityQueryHandler(IAuditLogRepository auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<AuditLogDto>>> Handle(
        GetAuditLogsByEntityQuery request,
        CancellationToken cancellationToken)
    {
        var auditLogs = await _auditLogRepository.GetByEntityAsync(
            request.EntityName,
            request.EntityId,
            cancellationToken);

        var dtos = _mapper.Map<List<AuditLogDto>>(auditLogs);
        return ApiResponse<List<AuditLogDto>>.Success(dtos);
    }
}
