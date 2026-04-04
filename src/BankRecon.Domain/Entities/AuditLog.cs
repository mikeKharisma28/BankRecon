namespace BankRecon.Domain.Entities;

/// <summary>
/// Stores audit trail entries for all create, update, and delete operations
/// performed on tracked entities in the database.
/// </summary>
public class AuditLog
{
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the entity that was affected (e.g., "ExampleSoftDeletableEntity").
    /// </summary>
    public string EntityName { get; set; } = string.Empty;

    /// <summary>
    /// The primary key value of the affected entity.
    /// </summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>
    /// The type of action performed: "Create", "Update", or "Delete".
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// JSON representation of the old values (null for Create operations).
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// JSON representation of the new values (null for Delete operations).
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// Comma-separated list of properties that were modified (for Update operations).
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
}
