using System;

namespace SixtyNamesTest.Commands
{
    public class CommandNameAttribute : Attribute
    {
        #region Constructor

        public CommandNameAttribute(string nameCommand)
        {
            NameCommand = nameCommand;
        }

        #endregion

        #region Properties

        public string NameCommand { get; }

        #endregion
    }
}
