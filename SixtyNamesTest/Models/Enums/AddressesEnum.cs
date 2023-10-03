using System.ComponentModel;

namespace SixtyNamesTest.Models.Enums
{
    public enum AddressesEnum
    {
        [CityAttribute(CitiesEnum.Moscow)]
        [Description("Адрес в Москве 1")]
        Address1,

        [CityAttribute(CitiesEnum.Moscow)]
        [Description("Адрес в Москве 2")]
        Address2,

        [CityAttribute(CitiesEnum.Samara)]
        [Description("Адрес в Самаре 1")]
        Address3,

        [CityAttribute(CitiesEnum.Samara)]
        [Description("Адрес в Самаре 2")]
        Address4,

        [CityAttribute(CitiesEnum.Tallin)]
        [Description("Адрес в Талине 1")]
        Address5,

        [CityAttribute(CitiesEnum.Tallin)]
        [Description("Адрес в Талине 2")]
        Address6,

        [CityAttribute(CitiesEnum.Minsk)]
        [Description("Адрес в Миске 1")]
        Address7,

        [CityAttribute(CitiesEnum.Minsk)]
        [Description("Адрес в Минске 2")]
        Address8,

        [CityAttribute(CitiesEnum.Erevan)]
        [Description("Адрес в Ереване 1")]
        Address9,

        [CityAttribute(CitiesEnum.Erevan)]
        [Description("Адрес в Ереване 2")]
        Address10,

        [CityAttribute(CitiesEnum.Brest)]
        [Description("Адрес в Бресте 1")]
        Address11,

        [CityAttribute(CitiesEnum.Brest)]
        [Description("Адрес в Бресте 2")]
        Address12
    }
}
