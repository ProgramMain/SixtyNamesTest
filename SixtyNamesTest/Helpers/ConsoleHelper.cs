using SixtyNamesTest.LoggingManagement;
using System;

namespace SixtyNamesTest.Helpers
{
    public sealed class ConsoleHelper : Singleton<ConsoleHelper>
    {
        #region Constructor

        private ConsoleHelper()
        {
            _logger = Logger.GetInstance();
        }

        #endregion

        #region Fields

        private readonly Logger _logger;

        #endregion

        #region Methods

        public void Clear() 
        {
            Console.Clear();
        }

        /// <summary>
        /// Прочитать строку из консоли
        /// </summary>
        /// <returns>Строка, которую вписал пользователь</returns>
        public string ReadString()
        {
            string readString = Console.ReadLine();
            if (!string.IsNullOrEmpty(readString))
                _logger.Info("Пользователь вводит : " + readString, out string error);

            return readString;
        }

        /// <summary>
        /// Показать пользователю приветствие в консоли
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns>Успешна ли операция</returns>
        public bool Welcome(out string error)
        {
            return WriteLine("Добро пожаловать!", ConsoleColor.White, out error);
        }

        /// <summary>
        /// Выводит текст на консоль под определенным цветом
        /// </summary>
        /// <param name="text">Выводимый текст</param>
        /// <param name="color">Цвет текста</param>
        /// <param name="error">Возвращаемая ошибка</param>
        /// <returns>Успешна ли операция</returns>
        public bool WriteLine(string text, ConsoleColor color, out string error)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);

                error = null;
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Отображает определенный текст в белом цвете и пишет в лог под тегом Info
        /// </summary>
        /// <param name="text">Текст, который необходимо отобразить</param>
        /// <param name="error">Ошибка в случае неудачи</param>
        /// <returns>Результат операции</returns>
        public bool Info(string text, out string error)
        {
            return WriteLine(text, ConsoleColor.White, out error) && _logger.Info($"console out -> {text}", out error);
        }

        public bool Warn(string text, out string error)
        {
            return WriteLine(text, ConsoleColor.Yellow, out error) && _logger.Warn($"console out -> {text}", out error);
        }

        public bool Alert(string text, out string error)
        {
            return WriteLine(text, ConsoleColor.Gray, out error) && _logger.Alert($"console out -> {text}", out error);
        }

        public bool Error(string text, out string error)
        {
            return WriteLine(text, ConsoleColor.Red, out error) && _logger.Error($"console out -> {text}", out error);
        }

        #endregion
    }
}
