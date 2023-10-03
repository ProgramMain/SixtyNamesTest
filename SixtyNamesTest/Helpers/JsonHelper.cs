using SixtyNamesTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SixtyNamesTest.Helpers
{
    public class JsonHelper : Singleton<JsonHelper>
    {
        #region Constructor

        private JsonHelper()
        {
                
        }

        #endregion

        #region Methods

        /// <summary>
        /// Создает файл json и записывает в него информацию
        /// </summary>
        /// <param name="models">Прочтенные модели для записи</param>
        /// <param name="error">Ошибка в случае возникновения</param>
        /// <returns></returns>
        public bool GenerateReportJson(List<ReportJsonModel> models, out string error)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                string textJson = JsonSerializer.Serialize(models, options);

                File.WriteAllText($"{DateTime.Now.ToString().Replace(':', '_').Replace(' ', '_')}.json", textJson);
                error = null;
                return true;
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
