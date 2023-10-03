using System.ComponentModel;

namespace SixtyNamesTest.Models.Enums
{
    public enum StatusContractEnum
    {
        [Description("Подписание")]
        Signing,
        [Description("Действующий")]
        Working,
        [Description("Расторгнут")]
        Close
    }
}
