using SixtyNamesTest.Models.Implementation;
using System.Collections.Generic;

namespace SixtyNamesTest.Models
{
    public class SumContractForRussiaReport : IReport
    {
        #region Constructor

        public SumContractForRussiaReport(Dictionary<string, decimal> dataReport)
        {
            DataReport = dataReport;
        }

        #endregion

        #region Properties

        public Dictionary<string, decimal> DataReport { get; }

        #endregion

        #region Methods

        public string ShowReport()
        {
            string showSring = string.Empty;
            foreach (var item in DataReport)
            {
                showSring += $"{item.Key} - {item.Value}\r\n";
            }

            return showSring;
        }

        #endregion
    }
}
