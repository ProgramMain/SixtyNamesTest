using SixtyNamesTest.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\getSumContratsForRussia")]
    [Description("Вывести сумму заключенных договоров по каждому контрагенту из России.")]
    internal class GetSumContratsForRussiaCommand : Command
    {
        public override bool Execute(out string error)
        {
            return DBHelper.GetSumContratsForRussia(out Dictionary<string, decimal> sumContractsForRussia, out error)
                && ConsoleHelper.Info($"Сумма заключенных договоров по каждому контрагенту из России:\r\n{new SumContractForRussiaReport(sumContractsForRussia).ShowReport()}", out error);
        }
    }
}
