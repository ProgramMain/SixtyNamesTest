using SixtyNamesTest.Helpers;
using SixtyNamesTest.Models.Enums;
using SixtyNamesTest.Models.Implementation;
using System;

namespace SixtyNamesTest.Models
{
    internal class Person : IPerson
    {
        #region Properties

        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public GenderEnum Gender { get; private set; }
        public byte Age { get; private set; }
        public ICompany Company { get; private set; }
        public CountryEnum Country { get; private set; }
        public CitiesEnum City { get; private set; }
        public AddressesEnum Address { get; private set; } 
        public IMail Email { get; private set; }
        public IPhone PhonePerson { get; private set; }
        public DateTime BirthDay { get; private set; }

        #endregion

        #region Methods

        public static Person Generate(string firstName, string surname, string patronymic, ICompany company)
        {
            var random = new Random();
            var addressesEnum = (AddressesEnum[])Enum.GetValues(typeof(AddressesEnum));

            var addressPerson = addressesEnum[random.Next(0, addressesEnum.Length)];
            var cityPerson = (CitiesEnum)addressPerson.GetCityId() - 1;
            var countryPerson = (CountryEnum)cityPerson.GetCountryId() - 1;

            DateTime date = DateTime.Parse($"{random.Next(1, 28)}.{random.Next(1, 13)}.{DateTime.Now.Year - random.Next(18, 100)}");

            return new Person()
            {
                FirstName = firstName,
                Surname = surname,
                Patronymic = patronymic,
                Company = company,
                Address = addressPerson,
                City = cityPerson,
                Country = countryPerson,
                BirthDay = date,
                Age = (byte)(DateTime.Now.Subtract(date).Days / 365),
                Email = Mail.Generate(firstName, false),
                Gender = (GenderEnum)random.Next(0, Enum.GetValues(typeof(GenderEnum)).Length),
                PhonePerson = Phone.Generate()
            };
        }

        #endregion
    }
}
