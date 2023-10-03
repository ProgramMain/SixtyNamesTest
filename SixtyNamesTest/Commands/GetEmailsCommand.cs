using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\getEmails")]
    [Description("Вывести список e-mail уполномоченных лиц, заключивших договора за последние 30 дней, на сумму больше 40000.")]
    public class GetEmailsCommand : Command
    {
        public override bool Execute(out string error)
        {
            return DBHelper.GetEmails(out string result, out error)
                && ConsoleHelper.Info($"Cписок e-mail уполномоченных лиц, заключивших договора за последние 30 дней, на сумму больше 40000: \r\n{result}", out error);
        }
    }
}
