using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRecon.Domain.Common.Interfaces;
public interface IHasKey
{
    /// <summary>
    /// Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}
