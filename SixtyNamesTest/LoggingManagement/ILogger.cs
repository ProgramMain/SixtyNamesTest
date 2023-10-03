namespace SixtyNamesTest.LoggingManagement
{
    public interface ILogger
    {
        bool Log(string logerPrefix, string text, out string error, string pref = null);
    }
}
