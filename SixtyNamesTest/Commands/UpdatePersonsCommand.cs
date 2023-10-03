using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\UpdatePersons")]
    [Description("Изменить статус договора на \"Расторгнут\" для физических лиц, у которых есть действующий договор, и возраст которых старше 60 лет включительно.")]
    public class UpdatePersonsCommand : Command
    {
        public override bool Execute(out string error)
        {
            return DBHelper.UpdatePersons(out int rows, out error)
                && ConsoleHelper.Info($"Количество измененных статусов договоров для физических лиц: {rows}", out error);
        }
    }
}
