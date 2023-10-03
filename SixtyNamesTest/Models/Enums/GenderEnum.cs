using System.ComponentModel;

namespace SixtyNamesTest.Models.Enums
{
    public enum GenderEnum
    {
        [Description("Мужчина")]
        Male = 0,
        [Description("Женщина")]
        Female = 1
    }
}
