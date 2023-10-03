using SixtyNamesTest.Models.Enums;
using SixtyNamesTest.Models.Implementation;
using System;

namespace SixtyNamesTest.Models
{
    public interface IPerson
    {
        string FirstName { get; }
        string Surname { get; }
        string Patronymic { get; }
        GenderEnum Gender { get; }
        byte Age { get; }
        ICompany Company { get; }
        CountryEnum Country { get; }
        CitiesEnum City { get; }
        AddressesEnum Address { get; }
        IMail Email { get; }
        IPhone PhonePerson { get; }
        DateTime BirthDay { get; }
    }
}
