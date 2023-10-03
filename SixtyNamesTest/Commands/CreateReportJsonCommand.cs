using SixtyNamesTest.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\CreateReportJson")]
    [Description("Создать отчет json содержащий ФИО, e-mail, моб. телефон, дату рождения физ. лиц, у которых есть действующие договора по компаниям, расположенных в городе Москва.")]
    public class CreateReportJsonCommand : Command
    {
        public override bool Execute(out string error)
        {
            return DBHelper.GetInfoForJsonReport(out List<ReportJsonModel> models, out error)
                && ConsoleHelper.Info("Данные выгружены! Начинаем выгрузку файла json...", out error)
                && JsonHelper.GenerateReportJson(models, out error)
                && ConsoleHelper.Info("Данные загружены в json файл", out error);
        }
    }
}
