using SixtyNamesTest.LoggingManagement.Behaviours;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace SixtyNamesTest.Helpers
{
    /// <summary>
    /// Класс отвечает за конфигурацию приложения
    /// </summary>
    internal sealed class ConfigurationHelper : Singleton<ConfigurationHelper>
    {
        #region Constructor

        private ConfigurationHelper()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; private set; }
        /// <summary>
        /// Была ли прочитана конфигурация
        /// </summary>
        public bool IsConfigurationReaded { get; private set; }
        /// <summary>
        /// Тип логирования для логера
        /// </summary>
        public ILoggerBehaviour LoggerBehaviour { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Прочитать конфигурацию
        /// </summary>
        /// <returns></returns>
        public bool ReadConfiguration(out string error)
        {
            lock(_syncRoot)
            {
                try
                {
                    if (IsConfigurationReaded)
                    {
                        error = "Конфигурация уже была прочитана!";
                        return false;
                    }

                    if (ConfigurationManager.ConnectionStrings.Count == 0)
                    {
                        error = "Нет не одной указанной строки подключения к БД!";
                        return false;
                    }

                    ConnectionString = ConfigurationManager.ConnectionStrings["SixtyNamesTestDBConnectionString"].ConnectionString;

                    string typeLogging = ConfigurationManager.AppSettings["TypeLogging"].ToString();

                    TypeInfo findTypeBegaviour = Assembly.GetAssembly(typeof(ILoggerBehaviour)).DefinedTypes
                        //Найдем все классы реализующие интерфейс расширения логгера
                        .Where(p => p.GetInterfaces().FirstOrDefault(d => d.Name == nameof(ILoggerBehaviour)) != null)
                        //Найдем из них те, у кого есть аттрибут поведения логгера
                        .Where(h => h.CustomAttributes.FirstOrDefault(j => j.AttributeType == typeof(BehaviourAttribute) && j.ConstructorArguments[0].Value.ToString() == typeLogging) != null)
                        //Выберем первый из них, который соответствует нашим критериям
                        .FirstOrDefault();

                    if (findTypeBegaviour != null)
                        LoggerBehaviour = (ILoggerBehaviour)Activator.CreateInstance(findTypeBegaviour.AsType());
                    else
                        LoggerBehaviour = new LoggerTagSeparationBehaviour();

                    IsConfigurationReaded = true;
                    error = null;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
        }

        #endregion
    }
}
