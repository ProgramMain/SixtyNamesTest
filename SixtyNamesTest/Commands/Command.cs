using SixtyNamesTest.Helpers;

namespace SixtyNamesTest.Commands
{
    public abstract class Command : ICommand
    {
        #region Constructor

        public Command()
        {
            ConsoleHelper = ConsoleHelper.GetInstance();
            CommandHelper = CommandHelper.GetInstance();
            DBHelper = DBHelper.GetInstance();
            JsonHelper = JsonHelper.GetInstance();
        }

        #endregion

        #region Properies

        public ConsoleHelper ConsoleHelper { get; }
        public CommandHelper CommandHelper { get; }
        public DBHelper DBHelper { get; }
        public JsonHelper JsonHelper { get; }

        #endregion

        #region Methods

        public abstract bool Execute(out string error);

        #endregion
    }
}
