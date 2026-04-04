using BankRecon.Application.Features.AuditLogs.Queries.GetAllAuditLogs;
using BankRecon.Application.Features.AuditLogs.Queries.GetAuditLogsByEntity;
using BankRecon.Shared.Common.Responses;
using BankRecon.Shared.Features.AuditLogs.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankRecon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all audit log entries.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<AuditLogDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllAuditLogsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get audit logs for a specific entity.
    /// </summary>
    /// <param name="entityName">The name of the entity (e.g., "Transaction")</param>
    /// <param name="entityId">The ID of the entity</param>
    [HttpGet("entity/{entityName}/{entityId}")]
    [ProducesResponseType(typeof(ApiResponse<List<AuditLogDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEntity(
        string entityName,
        string entityId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAuditLogsByEntityQuery(entityName, entityId),
            cancellationToken);
        return Ok(result);
    }
}
