using SixtyNamesTest.Helpers;
using SixtyNamesTest.Models.Enums;
using SixtyNamesTest.Models.Implementation;
using System;

namespace SixtyNamesTest.Models
{
    public class Company : ICompany
    {
        #region Properties

        public string CompanyName { get; private set; }
        public IINN INNCompany { get; private set; }
        public IOGRN OGRNCompany { get; private set; }
        public CountryEnum Country { get; private set; }
        public CitiesEnum City { get; private set; }
        public AddressesEnum Address { get; private set; }
        public IMail EmailCompany { get; private set; }
        public IPhone PhoneCompany { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Генерирует псевдо компанию
        /// </summary>
        /// <param name="name">Имя компании</param>
        /// <returns>Сгенерированная псевдокомпания</returns>
        public static Company Generate(string name)
        {
            var random = new Random();
            var addressesEnum = (AddressesEnum[])Enum.GetValues(typeof(AddressesEnum));

            var addressCompany = addressesEnum[random.Next(0, addressesEnum.Length)];
            var cityCompany = (CitiesEnum)addressCompany.GetCityId() - 1;
            var countryCompny = (CountryEnum)cityCompany.GetCountryId() - 1;

            return new Company()
            {
                CompanyName = name,
                INNCompany = INN.GenerateINN(),
                OGRNCompany = OGRN.Generate(),
                Address = addressCompany,
                City = cityCompany,
                Country = countryCompny,
                EmailCompany = Mail.Generate(name, true),
                PhoneCompany = Phone.Generate()
            };
        }

        #endregion
    }
}
