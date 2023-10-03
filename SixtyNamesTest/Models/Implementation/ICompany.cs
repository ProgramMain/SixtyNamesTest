using SixtyNamesTest.Models.Enums;
using SixtyNamesTest.Models.Implementation;

namespace SixtyNamesTest.Models
{
    public interface ICompany
    {
        string CompanyName { get; }
        IINN INNCompany { get; }
        IOGRN OGRNCompany { get; }
        CountryEnum Country { get; }
        CitiesEnum City { get; }
        AddressesEnum Address { get; }
        IMail EmailCompany { get; }
        IPhone PhoneCompany { get; }
    }
}
