using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\addInfoTables")]
    [Description("Заполняет таблицы рандомным содержимым для тестов (Запускайте несколько раз что бы разнообразить базу).")]
    internal class AddInformationForTablesCommand : Command
    {
        public override bool Execute(out string error)
        {
            return DBHelper.AddInformaion(out  error)
                && ConsoleHelper.Info("В базу добавлены рандомные значения.", out error);
        }
    }
}
