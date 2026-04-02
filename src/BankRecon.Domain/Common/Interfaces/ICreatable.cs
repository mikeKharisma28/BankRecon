namespace BankRecon.Domain.Common.Interfaces;

/// <summary>
/// Marks an entity as having creation tracking capability.
/// Implements who created the entity and when.
/// </summary>
public interface ICreatable
{
    DateTimeOffset CreatedAt { get; set; }
    string? CreatedBy { get; set; }
}
