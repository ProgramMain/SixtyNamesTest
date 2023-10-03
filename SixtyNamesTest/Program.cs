using System;
using SixtyNamesTest.Helpers;

namespace SixtyNamesTest
{
    internal class Program
    {
        static void Main()
        {
            var bootStrapper = BootSrapper.GetInstance();
            var consoleHelper = ConsoleHelper.GetInstance();

            if (!bootStrapper.StartProgramm(out string error) 
                || !bootStrapper.StopProgramm(out error))
            {
                consoleHelper.Error(error, out error);
                Console.ReadLine();
            }
        }
    }
}
