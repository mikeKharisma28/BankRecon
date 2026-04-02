using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankRecon.Domain.Common.Interfaces;

namespace BankRecon.Domain.Common;
public abstract class SoftDeletableEntity : BaseEntity, ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
