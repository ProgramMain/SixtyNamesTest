namespace SixtyNamesTest.LoggingManagement.Behaviours
{
    public interface ILoggerBehaviour
    {
        bool Write(string logerPrefix, string text, out string error, string pref = null);
    }
}
