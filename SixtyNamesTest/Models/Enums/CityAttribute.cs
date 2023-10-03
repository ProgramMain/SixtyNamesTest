using System;

namespace SixtyNamesTest.Models.Enums
{
    public class CityAttribute : Attribute
    {
        #region Constructor

        public CityAttribute(CitiesEnum city)
        {
            City = city;
        }

        #endregion

        #region Properties

        public CitiesEnum City { get; } 

        #endregion
    }
}
