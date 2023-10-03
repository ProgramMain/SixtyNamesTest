using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using SixtyNamesTest.Helpers;

namespace SixtyNamesTest.Commands
{
    [CommandNameAttribute("\\h")]
    [Description("Вывести на консоль все команды.")]
    internal class HelpCommand : Command
    {
        #region Methods

        /// <summary>
        /// Выполнить команду помощи
        /// </summary>
        /// <param name="error">Возвращаемая ошибка</param>
        /// <returns>Результат операции</returns>
        public override bool Execute(out string error)
        {
            try
            {
                TypeInfo[] findTypeBegaviour = Assembly.GetAssembly(typeof(ICommand)).DefinedTypes
                        //Найдем все классы реализующие интерфейс команды
                        .Where(p => p.GetInterfaces().FirstOrDefault(d => d.Name == nameof(ICommand)) != null).Reverse().ToArray();

                string helpCommandText = string.Empty;

                foreach(var  type in findTypeBegaviour)
                {
                    var attributeName = type.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(CommandNameAttribute));
                    var attributeDescription = type.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(DescriptionAttribute));

                    if (attributeName != null && attributeDescription != null)
                        helpCommandText += $"{attributeName.ConstructorArguments[0].ToString().Replace("\"", "")} - {attributeDescription.ConstructorArguments[0].ToString().Replace("\"", "")}\r\n";
                }

                error = null;
                return ConsoleHelper.Info(helpCommandText, out error);
            }
            catch(Exception ex) 
            {
                error = ex.Message;
                return false;
            }
        }

        #endregion
    }
}
