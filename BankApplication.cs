using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.DataAccess;
using Day5.Helpers;
using Day5.Interfaces;
using Day5.Models;
using Day5.Services;

namespace Day5
{
   public class BankApplication
    {
        private readonly StringBuilder _stringBuilder;
        private readonly ILogger _logger;
        private readonly IMenu _menu;
        private readonly IService<Bank> _bankService;

        public BankApplication(ILogger logger)
        {
            _stringBuilder = new StringBuilder();
            _logger = logger;
            _menu = new BankMenu(logger);
            _bankService = new BankService();
        }

        public void Run()
        {
            var running = true;
            mainmenu:
                _stringBuilder.Clear();
                _stringBuilder.AppendLine("Press:");
                _stringBuilder.AppendLine("1. Get All Banks");
                _stringBuilder.AppendLine("2. Get All Banks In A Particular Continent");
                _stringBuilder.AppendLine("3. Add A New Bank");
                _stringBuilder.AppendLine("4. Update An Existing Bank");
                _stringBuilder.AppendLine("5. Back to Main Menu");


            while (running)
            {
                _menu.Display(_stringBuilder.ToString());

                switch (Console.ReadLine())
                {
                    case "1":
                        GetAllBanks();
                        DisplayPrompt();
                        if (Console.ReadLine() == "1")
                        {
                            goto mainmenu;

                        }
                        else
                        {

                            running = false;
                            Application.Run();
                           
                            break;
                        }
                       
                    case "2":
                        GetAllBanksInAContinent();
                        DisplayPrompt();
                        if (Console.ReadLine() == "1")
                        {
                            goto mainmenu;
                        }
                        else
                        {
                            running = false;
                        }
                        break;


                    case "3":
                        New();
                        DisplayPrompt();
                        if (Console.ReadLine() == "1")
                        {
                            goto mainmenu;
                        }
                        else
                        {
                            running = false;
                        }
                        break;


                    case "4":
                        Update();
                        DisplayPrompt();
                        if (Console.ReadLine() == "1")
                        {
                            goto mainmenu;
                        }
                        else
                        {
                            running = false;
                        }
                        break;

                    case "5":
                        Application.Run();
                        running = false;
                        break;
                    default:
                        _logger.LogLine("\nInvalid input...\nTry Again!!\n");
                        break;
                    

                }

            }

        }

        private  void GetAllBanks()
        {
            foreach (var keyValuePair in _bankService.GetAll())
            {
                _logger.LogLine($"\n{keyValuePair.Key}\n-------------");
                FetchBanks(keyValuePair.Value);
                _logger.LogLine("-------------");
            }
        }

        private  void GetAllBanksInAContinent()
        {
            while (true)
            {
                _menu.Display("Select a continent to display:\n1. Africa\n2. Europe\n3. Asia");

                switch (Console.ReadLine())
                {
                    case "1":
                        _logger.LogLine("Banks In Africa:");
                        FetchBanks(_bankService.GetAll("Africa"));
                        break;

                    case "2":
                        _logger.LogLine("Banks In Europe:");
                        FetchBanks(_bankService.GetAll("Europe"));
                        break;

                    case "3":
                        _logger.LogLine("Banks In Asia:");
                        FetchBanks(_bankService.GetAll("Asia"));
                        break;
                    default:
                        _logger.Log("Invalid Input!\nTry Again!");
                        break;
                }
            }
            
        }

        private  void CreateNewBank(string continent)
        {
            var model = new CreateBankViewModel();
            _logger.LogLine("Enter the New Value for Bank Name");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                model.Name = name;
            else
                _logger.Log("Blank Inputs not allowed!\nTry Again!");

            _logger.LogLine("Enter the New Value for Region");
            var region = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(region))
                model.Region = region;
            else
                _logger.Log("Blank Inputs not allowed!\nTry Again!");

            var length = _bankService.GetAll(continent).Count();
            _bankService.Add(continent, new Bank { Id = length, Name = name, Region = region });
        }


        private  void UpdateBank(string continent, int index)
        {
            var model = new UpdateBankViewModel();

            var bankToUpdate = _bankService.Get(continent, index);

            _logger.LogLine("Enter new value for Name or blank to Ignore");
            var name = Console.ReadLine();
           
            if (!string.IsNullOrWhiteSpace(name))
            {
                model.Name = name;

                bankToUpdate.Name = model.Name;
            }
            
            _logger.LogLine("Enter the New Value for Region");
            var region = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(region)) return;
            model.Region = region;
            bankToUpdate.Region = model.Region;
        }


        private  void New()
        {
            _menu.Display("Select the continent :\n1. Africa\n2. Europe\n3. Asia");
            
            switch (Console.ReadLine())
            {
                case "1":
                    CreateNewBank("Africa");
                    break;

                case "2":
                    CreateNewBank("Europe");
                    break;

                case "3":
                    CreateNewBank("Asia");
                    break;
                default:
                    _logger.Log("Invalid Input!\nTry Again!");
                    break;
            }

        }


        private  void Update()
        {
            _menu.Display("Select the continent :\n1. Africa\n2. Europe\n3. Asia");

            switch (Console.ReadLine())
            {

                case "1":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchBanks(_bankService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input))
                    {
                        UpdateBank("Africa", input);
                    }
                    else
                    {
                        _logger.Log("Invalid Input!\nTry Again!");
                    }
                    break;

                case "2":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchBanks(_bankService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input1))
                    {
                        UpdateBank("Europe", input1);
                    }
                    else
                    {
                        _logger.Log("Invalid Input!\nTry Again!");
                    }
                    break;

                case "3":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchBanks(_bankService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input2))
                    {
                        UpdateBank("Asia", input2);
                    }
                    else
                    {
                        _logger.Log("Invalid Input!\nTry Again!");
                    }
                    break;
                    
                default:
                    _logger.Log("Invalid Input!\nTry Again!");
                    break;
            }

        }


        private void DisplayPrompt()
        {
            _menu.Display("Go to Bank Menu ?\n1. Yes\n2. Any other Key to go back main menu!");
        }

        private void FetchBanks(IEnumerable<Bank> banks)
        {
            foreach (var bank in banks)
            {
                _logger.LogLine($"Id: {bank.Id}\nName: {bank.Name}\nRegion: {bank.Region}\n");
            }
        }

    }
}
