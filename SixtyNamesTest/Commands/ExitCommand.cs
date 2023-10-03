using System;
using System.ComponentModel;
using SixtyNamesTest.Helpers;

namespace SixtyNamesTest.Commands
{
    [CommandName("\\exit")]
    [Description("Выход из программы")]
    internal class ExitCommand : Command
    {
        #region Methods

        public override bool Execute(out string error)
        {
            try
            {
                CommandHelper.IsEnabled = false;

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
