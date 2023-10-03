using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtyNamesTest.Models.Enums
{
    public enum CitiesEnum
    {
        [Country(CountryEnum.Russia)]
        [Description("Москва")]
        Moscow,

        [Country(CountryEnum.Russia)]
        [Description("Самара")]
        Samara,

        [Country(CountryEnum.Belarus)]
        [Description("Минск")]
        Minsk,

        [Country(CountryEnum.Belarus)]
        [Description("Брест")]
        Brest,

        [Country(CountryEnum.Armenia)]
        [Description("Ереван")]
        Erevan,

        [Country(CountryEnum.Armenia)]
        [Description("Талин")]
        Tallin
    }
}
