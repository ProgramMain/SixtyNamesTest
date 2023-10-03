using System;
using System.Linq;
using System.Reflection;
using SixtyNamesTest.Commands;
using SixtyNamesTest.LoggingManagement;
using ICommand = SixtyNamesTest.Commands.ICommand;

namespace SixtyNamesTest.Helpers
{
    public sealed class CommandHelper : Singleton<CommandHelper>
    {
        #region Constructor

        private CommandHelper()
        {
            _logger = Logger.GetInstance();
            _consoleHelper = ConsoleHelper.GetInstance();
            IsEnabled = true;
        }

        #endregion

        #region Fileds

        private readonly Logger _logger;
        private readonly ConsoleHelper _consoleHelper;

        #endregion

        #region Properties

        public bool IsEnabled { get; set; }

        #endregion

        #region Mehods

        /// <summary>
        /// Запустить прослушку команд
        /// </summary>
        /// <param name="commandText">текст команды</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool ReadRunCommands(string commandText, out string error)
        {
            try
            {
                if (!RunCommand(commandText, out error))
                    _consoleHelper.Error(error, out error);

                if (IsEnabled)
                    return _consoleHelper.Info("Введите команду:", out error) && ReadRunCommands(_consoleHelper.ReadString(), out error);

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Запустить команду на выполнение
        /// </summary>
        /// <param name="command">Название команды</param>
        /// <param name="error">Возвращаемая ошибка</param>
        /// <returns>Успешна ли операция</returns>
        public bool RunCommand(string command, out string error)
        {
            try
            {
                //проверка
                if(string.IsNullOrEmpty(command))
                {
                    error = "Команда не определена!";
                    return false;
                }

                //Залогируем
                if (!_logger.Info($"Запуск команды {command}...", out error))
                    return false;

                //Сначала найдем все команды
                var typesCommands = Assembly.GetExecutingAssembly()
                    .DefinedTypes
                    .Where(p => typeof(Command).IsAssignableFrom(p))
                    .ToList();

                //Найдем команду схожую по атрибуту
                var findCommand = typesCommands
                    .FirstOrDefault(p => p.CustomAttributes
                    .FirstOrDefault(j => j.ConstructorArguments != null && j.ConstructorArguments.Count > 0 && j.ConstructorArguments[0].Value.ToString() == command) != null);

                //Если команду не нашли - скажем это пользователю
                if(findCommand == null)
                {
                    error = "Команда не найдена! \\h - для перечня команд!";
                    _consoleHelper.Warn(error, out error);
                    return true;
                }

                //Создадим команду и запустим
                var commandInst = (ICommand)Activator.CreateInstance(findCommand);
                return commandInst.Execute(out error) && _logger.Info($"Отработана команда {command}...", out error);
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
