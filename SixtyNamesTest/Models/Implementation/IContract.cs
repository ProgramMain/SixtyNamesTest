using SixtyNamesTest.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtyNamesTest.Models
{
    public interface IContract
    {
        ICompany Company { get; }
        IPerson Person { get; }
        decimal Amount { get; }
        StatusContractEnum Status { get; }
        DateTime SigningDate { get; }
    }
}
