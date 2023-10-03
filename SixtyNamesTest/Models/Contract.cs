using System;
using SixtyNamesTest.Models.Enums;

namespace SixtyNamesTest.Models
{
    internal class Contract : IContract
    {
        #region Properties

        public ICompany Company { get; private set; }
        public IPerson Person { get; private set; }
        public decimal Amount { get; private set; }
        public StatusContractEnum Status { get; private set; }
        public DateTime SigningDate { get; private set; }


        #endregion

        #region Methods

        public static Contract Generate(ICompany company, IPerson person)
        {
            var random = new Random();

            return new Contract()
            {
                Company = company,
                Person = person,
                Amount = random.Next(3000, 100000),
                SigningDate = person.BirthDay.AddYears(21),
                Status = (StatusContractEnum)random.Next(0, Enum.GetValues(typeof(StatusContractEnum)).Length)
            };
        }

        #endregion
    }
}
