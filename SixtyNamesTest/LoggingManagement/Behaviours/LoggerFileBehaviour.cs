using System;
using System.IO;

namespace SixtyNamesTest.LoggingManagement.Behaviours
{
    public abstract class LoggerFileBehaviour : ILoggerBehaviour
    {
        private static object _syncRoot = new object();

        public virtual bool Write(string fileName, string text, out string error, string pref = null)
        {
            try
            {
                Directory.CreateDirectory(Logger.NameDirectoryLog);

                lock (_syncRoot)
                    File.AppendAllText(fileName, text);

                error = null;
                return true;
            }
            catch(Exception ex) 
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
