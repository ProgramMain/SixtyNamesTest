using System.Diagnostics;
using SixtyNamesTest.Helpers;
using SixtyNamesTest.LoggingManagement.Behaviours;

namespace SixtyNamesTest.LoggingManagement
{
    internal sealed class Logger : Singleton<Logger>, ILogger
    {
        #region Consts

        public const string FormatFileLog = ".log";
        public const string NameDirectoryLog = "Logs";
        public const string StartPropramm = "<------Start program------>";
        public const string StopPropramm = "<------Stop program------>";

        #endregion

        #region Constructor

        private Logger()
        {

        }

        #endregion

        #region Properties

        public ILoggerBehaviour LoggerBehaviour { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Логирует старт программы в логи
        /// </summary>
        /// <param name="error"></param>
        /// <returns>Успешна ли операция логирования</returns>
        public bool StartProgrammLog(out string error)
        {
            return (LoggerBehaviour is LoggerTagSeparationBehaviour) ? 
                (Info(StartPropramm, out error)
                && Error(StartPropramm, out error)
                && Warn(StartPropramm, out error)
                && Alert(StartPropramm, out error)
                && Debug(StartPropramm, out error))
                : Info(StartPropramm, out error);
        }

        public bool StopProgrammLog(out string error)
        {
            return (LoggerBehaviour is LoggerTagSeparationBehaviour) ?
                 (Info(StopPropramm, out error)
                 && Error(StopPropramm, out error)
                 && Warn(StopPropramm, out error)
                 && Alert(StopPropramm, out error)
                 && Debug(StopPropramm, out error))
                 : Info(StopPropramm, out error);
        }

        public bool Log(string loggerPrefix, string text, out string error, string pref = null)
        {
            lock (_syncRoot)
            {
                string name = pref ?? "SHELL";

                if (LoggerBehaviour == null)
                    LoggerBehaviour = new LoggerTagSeparationBehaviour();

                return LoggerBehaviour.Write(loggerPrefix, text, out error, name);
            }
        }

        public bool Info(string text, out string error)
        {
            return Log("INFO", text, out error, new StackTrace(true)?.GetFrames()[1].GetMethod().Name);
        }

        public bool Debug(string text, out string error)
        {
            return Log("DEBUG", text, out error, new StackTrace(true)?.GetFrames()[1].GetMethod().Name);
        }

        public bool Warn(string text, out string error)
        {
            return Log("WARN", text, out error, new StackTrace(true)?.GetFrames()[1].GetMethod().Name);
        }

        public bool Alert(string text, out string error)
        {
            return Log("ALERT", text, out error, new StackTrace(true)?.GetFrames()[1].GetMethod().Name);
        }

        public bool Error(string text, out string error)
        {
            return Log("ERROR", text, out error, new StackTrace(true)?.GetFrames()[1].GetMethod().Name);
        }

        /// <summary>
        /// Задать поведение логера
        /// </summary>
        /// <param name="loggerBehaviour"></param>
        /// <returns>Успешно ли задано поведение</returns>
        public bool SetBehaviour(ILoggerBehaviour loggerBehaviour)
        {
            lock (_syncRoot)
            {
                LoggerBehaviour = loggerBehaviour;
            }

            return true;
        }

        #endregion
    }
}
