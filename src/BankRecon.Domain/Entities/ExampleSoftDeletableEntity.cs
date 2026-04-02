using BankRecon.Domain.Common;

namespace BankRecon.Domain.Entities;

/// <summary>
/// Transaction entity with full audit trail and soft delete capability.
/// </summary>
public class ExampleSoftDeletableEntity : SoftDeletableEntity
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
}
