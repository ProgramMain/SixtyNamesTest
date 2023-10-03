using SixtyNamesTest.Models.Implementation;

namespace SixtyNamesTest.Models
{
    public class Mail : IMail
    {
        #region Properties

        public string MailText { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Генерация псевдо почтового ящика компании
        /// </summary>
        /// <param name="nameCompany"></param>
        /// <returns></returns>
        public static Mail Generate(string name, bool isCompany)
        {
            if (isCompany)
            {
                return new Mail()
                {
                    MailText = "press_company@" + name + ".ru"
                };
            }

            return new Mail()
            {
                MailText = name + "@Mail.ru"
            };
        }

        #endregion

    }
}
