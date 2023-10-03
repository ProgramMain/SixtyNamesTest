using SixtyNamesTest.Models.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtyNamesTest.Models
{
    public class INN : IINN
    {
        #region Properties

        public string INNText { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Метод генерирует не настоящие INN для простоты реализации
        /// </summary>
        /// <returns>Псевдо ИНН</returns>
        public static INN GenerateINN()
        {
            var random = new Random();

            return new INN()
            {
                INNText = random.Next(1, 100000).ToString() + random.Next(1, 100000).ToString(),
            };
        }

        #endregion
    }
}
