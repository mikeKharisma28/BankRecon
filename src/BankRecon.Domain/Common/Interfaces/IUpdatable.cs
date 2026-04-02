namespace BankRecon.Domain.Common.Interfaces;

/// <summary>
/// Marks an entity as having update tracking capability.
/// Implements who last updated the entity and when.
/// </summary>
public interface IUpdatable
{
    DateTimeOffset? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}
