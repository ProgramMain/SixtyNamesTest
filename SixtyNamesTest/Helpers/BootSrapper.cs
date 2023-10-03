using System;
using SixtyNamesTest.LoggingManagement;

namespace SixtyNamesTest.Helpers
{
    /// <summary>
    /// Класс отвечает за корректный запуск программы 
    /// </summary>
    internal sealed class BootSrapper : Singleton<BootSrapper>
    {
        #region Constructor

        private BootSrapper()
        {
            _configurationHelper = ConfigurationHelper.GetInstance();
            _logger = Logger.GetInstance();
            _consoleHelper = ConsoleHelper.GetInstance();
            _commandHelper = CommandHelper.GetInstance();
            _dbHelper = DBHelper.GetInstance();
        }

        #endregion

        #region Fields

        private readonly ConfigurationHelper _configurationHelper;
        private readonly Logger _logger;
        private readonly ConsoleHelper _consoleHelper;
        private readonly CommandHelper _commandHelper;
        private readonly DBHelper _dbHelper;

        #endregion

        #region Methods

        /// <summary>
        /// Запуск программы
        /// </summary>
        /// <returns>Успешен ли запуск программы</returns>
        public bool StartProgramm(out string error)
        {
            try
            {
                //Поприветствуем пользователя
                return _consoleHelper.Welcome(out error)
                    // Выведем информацию 
                    && _consoleHelper.WriteLine("Чтение конфигурации...", ConsoleColor.White, out error)
                    // Прочитаем конфигурацию
                    && _configurationHelper.ReadConfiguration(out error)
                    // Установим принцип логирования логеру
                    && _logger.SetBehaviour(_configurationHelper.LoggerBehaviour)
                    // Залогируем запуск программы
                    && _logger.StartProgrammLog(out error)
                    // Залогируем и выведем в консоль информацию
                    && _consoleHelper.Info("Конфигурация прочтена, логер запущен!", out error)
                    && _consoleHelper.Info("Подключение к базе данных...", out error)
                    // Подключимся к базе данных
                    && _dbHelper.Connect(out error)
                    // Залогируем и выведем в консоль информацию
                    && _consoleHelper.Info("Подключено к базе данных", out error)
                    && _consoleHelper.Info("Создание всех необходимых таблиц...", out error)
                    // Создадим все необходимые таблицы
                    && _dbHelper.CreateTables(out error)
                    // Залогируем и выведем в консоль информацию
                    && _consoleHelper.Info("Все таблицы созданы", out error)
                    && _consoleHelper.Info("Заполнние всех вспомогательных таблиц...", out error)
                    // Заполним все вспомогательные таблицы
                    && _dbHelper.InsertInformationTables(out error)
                    // Залогируем и выведем в консоль информацию
                    && _consoleHelper.Info("Все таблицы заполнены", out error)
                    && _consoleHelper.Info("\\h - перечень всех команд", out error)
                    && _consoleHelper.Info("Введите команду:", out error)
                    // Начнем работу с командами пользователя
                    && _commandHelper.ReadRunCommands(_consoleHelper.ReadString(), out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Остановка программы
        /// </summary>
        /// <returns>Успешна ли остановка программы</returns>
        public bool StopProgramm(out string error) 
        {
            try
            {
                return _consoleHelper.Info("Дождитесь корректного завершения работы программы...", out error)
                    // Отключимся от базы
                    && _dbHelper.Disconnect(out error)
                    // Залогируем остановку программы
                    && _logger.StopProgrammLog(out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
