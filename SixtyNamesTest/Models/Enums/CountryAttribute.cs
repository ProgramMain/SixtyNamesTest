using System;

namespace SixtyNamesTest.Models.Enums
{
    public class CountryAttribute : Attribute
    {
        #region Constructor

        public CountryAttribute(CountryEnum country)
        {
            Country = country;
        }

        #endregion

        #region Properties

        public CountryEnum Country { get; }

        #endregion
    }
}
