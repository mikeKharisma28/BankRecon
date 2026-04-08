using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankRecon.Bsui.Client.Common.Options;
public class BackEndOptions
{
    public const string SectionKey = "BackEnd";

    public string BaseUrl { get; set; } = default!;
}
