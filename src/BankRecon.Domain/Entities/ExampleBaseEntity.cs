using BankRecon.Domain.Common;

namespace BankRecon.Domain.Entities;

/// <summary>
/// Entity with full audit trail capability.
/// </summary>
public class ExampleBaseEntity : BaseEntity
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
}
