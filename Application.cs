using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.Helpers;
using Day5.Interfaces;
using Day5.Services;

namespace Day5
{
    public static class Application
    {
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        private static readonly ILogger Logger = new Logger();
        private static readonly IMenu MainMenu = new MainMenu(Logger);
        private static readonly BankApplication BankApplication = new BankApplication(Logger);
        private static readonly UserApplication UserApplication = new UserApplication(Logger);
       
        public static void Run()
        {
           

            mainmenu:
                StringBuilder.Clear();
                StringBuilder.AppendLine("Welcome to the Generic Management System");
                StringBuilder.AppendLine("Press:");
                StringBuilder.AppendLine("1. Bank Management System");
                StringBuilder.AppendLine("2. User Management System");
                StringBuilder.AppendLine("3. Exit");

            var running = true;

            while (running)
            {
                MainMenu.Display(StringBuilder.ToString());
                switch (Console.ReadLine())
                {
                    case "1":
                        BankApplication.Run();
                        DisplayPrompt();
                        if (Console.ReadLine() == "1")
                            goto mainmenu;
                        else
                            running = false;
                        break;

                    case "2":

                        UserApplication.Run();
                        DisplayPrompt();

                        if (Console.ReadLine() == "1")
                            goto mainmenu;
                        else
                            running = false;
                        break;

                    case "3":
                        running = false;
                        Logger.LogLine("\nThanks and GoodBye!...\nRun the App to use again!...\n");
                        break;
                      
                      
                    default:
                        Logger.LogLine("\nInvalid input...\nTry Again!!\n");
                        goto mainmenu;
                }
            }


           
        }

         private static void DisplayPrompt()
            {
                MainMenu.Display("\nDo you want to perform another operation? \n1. Yes\n2. Any other Key to exit!");
            }
    }
}
