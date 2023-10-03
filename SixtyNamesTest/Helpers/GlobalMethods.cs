using SixtyNamesTest.Models.Enums;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SixtyNamesTest.Helpers
{
    public static class GlobalMethods
    {
        public static int GetCityId<T>(this T e) where T : IConvertible
        {
            if (e == null)
                throw new ArgumentNullException("Параметр 'e' должен быть определен!");

            if (e.GetType() != typeof(AddressesEnum))
                throw new ArgumentNullException("Выбрать страну можно только у города!");

            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var cityAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(CityAttribute), false)
                        .FirstOrDefault() as CityAttribute;

                    if (cityAttribute != null)
                    {
                        return ((int)cityAttribute.City) + 1;
                    }
                }
            }
            return 0;
        }

        public static int GetCountryId<T>(this T e) where T : IConvertible
        {
            if(e == null) 
                throw new ArgumentNullException("Параметр 'e' должен быть определен!");

            if(e.GetType() != typeof(CitiesEnum))
                throw new ArgumentNullException("Выбрать страну можно только у города!");

            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var countryAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(CountryAttribute), false)
                        .FirstOrDefault() as CountryAttribute;

                    if (countryAttribute != null)
                    {
                        return ((int)countryAttribute.Country) + 1;
                    }
                }
            }
            return 0;
        }
            
 

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
                return string.Empty;
            }
            return null;
        }
    }
}
