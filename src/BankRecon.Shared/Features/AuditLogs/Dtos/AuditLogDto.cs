using AutoMapper;
using BankRecon.Domain.Entities;
using BankRecon.Shared.Common.Mappings;

namespace BankRecon.Shared.Features.AuditLogs.Dtos;

/// <summary>
/// Data transfer object for audit log entries.
/// Provides a read-only view of audit trail data.
/// </summary>
public class AuditLogDto : IMapFrom<AuditLog>
{
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the entity that was affected.
    /// </summary>
    public string EntityName { get; set; } = string.Empty;

    /// <summary>
    /// The primary key value of the affected entity.
    /// </summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>
    /// The type of action: "Create", "Update", or "Delete".
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// JSON representation of the old values.
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// JSON representation of the new values.
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// Comma-separated list of properties that were modified.
    /// </summary>
    public string? AffectedColumns { get; set; }

    /// <summary>
    /// Timestamp when the action occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// The user who performed the action.
    /// </summary>
    public string? PerformedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuditLog, AuditLogDto>();
    }
}
