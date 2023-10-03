using System;
using System.IO;

namespace SixtyNamesTest.LoggingManagement.Behaviours
{
    [Behaviour("TagSeparation")]
    public class LoggerTagSeparationBehaviour : LoggerFileBehaviour
    {
        public override bool Write(string loggerPrefix, string text, out string error, string pref = null)
        {
            string name = pref ?? "SHELL";

            string formattedLoggerPrefix = loggerPrefix ?? "INFO";

            return base.Write(Path.Combine(Logger.NameDirectoryLog, formattedLoggerPrefix + Logger.FormatFileLog), $"{DateTime.Now}\t{name}\t{formattedLoggerPrefix}\t{text}\r\n", out error);
        }
    }
}
