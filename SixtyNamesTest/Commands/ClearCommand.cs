using System;
using SixtyNamesTest.Helpers;
using System.ComponentModel;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\clear")]
    [Description("Очистить консоль")]
    internal class ClearCommand : Command
    {
        #region Methods

        public override bool Execute(out string error)
        {
            try
            {
                ConsoleHelper.Clear();

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
