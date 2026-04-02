namespace BankRecon.Domain.Common.Interfaces;

/// <summary>
/// Marks an entity as soft-deletable. Soft-deleted entities
/// are marked as deleted but not removed from the database.
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}
