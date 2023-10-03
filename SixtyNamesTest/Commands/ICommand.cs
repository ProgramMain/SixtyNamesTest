namespace SixtyNamesTest.Commands
{
    public interface ICommand
    {
        bool Execute(out string error);
    }
}
