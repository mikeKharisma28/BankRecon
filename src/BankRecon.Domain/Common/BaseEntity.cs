using BankRecon.Domain.Common.Interfaces;

namespace BankRecon.Domain.Common;

/// <summary>
/// Base entity class for all domain entities. Provides unique identifier.
/// Entities can implement IAuditable, ICreatable, IUpdatable, and/or ISoftDeletable
/// interfaces to opt into those tracking features.
/// </summary>
public abstract class BaseEntity : IHasKey, ICreatable, IUpdatable
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
