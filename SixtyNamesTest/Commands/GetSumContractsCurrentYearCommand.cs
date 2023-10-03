using System;
using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\getSumContractsCurrentYear")]
    [Description("Вывести сумму всех заключенных договоров за текущий год.")]
    internal class GetSumContractsCurrentYearCommand : Command
    {
        public override bool Execute(out string error)
        {
            try
            {
                return DBHelper.GetSumContractsCurrentYear(out decimal sumContract, out error)
                    && ConsoleHelper.Info($"Cумма всех заключенных договоров за текущий год : {sumContract}", out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
