using System;
using System.IO;

namespace SixtyNamesTest.LoggingManagement.Behaviours
{
    [Behaviour("OneDayOneFile")]
    public class LoggerOneDayOneFileBehaviour : LoggerFileBehaviour
    {
        public override bool Write(string loggerPrefix, string text, out string error, string pref = null)
        {
            string name = pref ?? "SHELL";
            string formattedLoggerPrefix = loggerPrefix ?? "INFO";

            return base.Write(Path.Combine(Logger.NameDirectoryLog, DateTime.Now.ToString("dd.MM.yy") + Logger.FormatFileLog), $"{DateTime.Now}\t{name}\t{formattedLoggerPrefix}\t{text}\r\n", out error);
        }
    }
}
