using SixtyNamesTest.Models.Implementation;
using System;

namespace SixtyNamesTest.Models
{
    public class Phone : IPhone
    {
        #region Properties

        public string PhoneText { get; private set; }

        #endregion

        #region Metods

        /// <summary>
        /// Пробует пробразовать строку в объект PoneModel
        /// </summary>
        /// <param name="text">Строка содержащая телефон формата +7xxxxxxxxxx</param>
        /// <param name="phoneModel">распаршенная строка</param>
        /// <returns>Возможно ли распарсить данную строку в телефон</returns>
        public static bool TryParse(string text, out Phone phoneModel)
        {
            bool isParse = !string.IsNullOrEmpty(text) 
                && text.StartsWith("+7") 
                && text.Length == 12;

            if(isParse)
                phoneModel = new Phone() { PhoneText = text };
            else
                phoneModel = null;

            return isParse;
        }

        public static Phone Generate()
        {
            var random = new Random();
            return new Phone() 
            { 
                PhoneText = "+7" + random.Next(1, 1000000000) + random.Next(1, 10) 
            };
        }

        #endregion
    }
}
