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
    public class UserApplication
    {
        private readonly StringBuilder _stringBuilder;
        private readonly ILogger _logger;
        private readonly IMenu _menu;
        private readonly IService<User> _userService;


        public UserApplication(ILogger logger)
        {
            _stringBuilder = new StringBuilder();
            _logger = logger;
            _menu = new UserMenu(logger);
            _userService = new UserService();
        }

        public void Run()
        {
            var running = true;
        mainmenu:
            _stringBuilder.Clear();
            _stringBuilder.AppendLine("Press:");
            _stringBuilder.AppendLine("1. Get All Users");
            _stringBuilder.AppendLine("2. Get All Users In A Particular Country");
            _stringBuilder.AppendLine("3. Add A New User");
            _stringBuilder.AppendLine("4. Update An Existing User");
            _stringBuilder.AppendLine("5. Back to Main Menu");


            while (running)
            {
                _menu.Display(_stringBuilder.ToString());

                switch (Console.ReadLine())
                {
                    case "1":
                        GetAllUsers();
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
                        GetAllUsersInAContinent();
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

        private void GetAllUsers()
        {
            foreach (var keyValuePair in _userService.GetAll())
            {
                _logger.LogLine($"\n{keyValuePair.Key}\n-------------");
                FetchUsers(keyValuePair.Value);
                _logger.LogLine("-------------");
            }
        }

        private void GetAllUsersInAContinent()
        {
            while (true)
            {
                _menu.Display("Select a continent to display:\n1. Africa\n2. Europe\n3. Asia");

                switch (Console.ReadLine())
                {
                    case "1":
                        _logger.LogLine("Users In Africa:");
                        FetchUsers(_userService.GetAll("Africa"));
                        break;

                    case "2":
                        _logger.LogLine("Users In Europe:");
                        FetchUsers(_userService.GetAll("Europe"));
                        break;

                    case "3":
                        _logger.LogLine("Users In Asia:");
                        FetchUsers(_userService.GetAll("Asia"));
                        break;
                    default:
                        _logger.Log("Invalid Input!\nTry Again!");
                        break;
                }
            }

        }

        private void CreateNewUser(string continent)
        {
            var model = new CreateUserViewModel();

            _logger.LogLine("Enter the New Value for user FirstName");
            var firstname = Console.ReadLine();

            _logger.LogLine("Enter the New Value for User Lastname");
            var lastname = Console.ReadLine();

            _logger.LogLine("Enter the New Value for User Country");
            var userCountry = Console.ReadLine();

            while(NumberChecker.isNumber(firstname) || NumberChecker.isNumber(lastname) ||
                NumberChecker.isNumber(userCountry))
            {
                _logger.LogLine("No numbers ,whitespace or letters allowed");

                _logger.LogLine("Enter the New Value for user FirstName");
                firstname = Console.ReadLine();

                _logger.LogLine("Enter the New Value for User Lastname");
                lastname = Console.ReadLine();

                _logger.LogLine("Enter the New Value for User Country");
                userCountry = Console.ReadLine();
            }

            var fullname = $"{firstname} {lastname}";

            var length = _userService.GetAll(continent).Count();
            _userService.Add(continent, new User { Id = length, Name = fullname, Country = userCountry });
        }


        private void UpdateUser(string continent, int index)
        {
            var model = new UpdateUserViewModel();

            var UserToUpdate = _userService.Get(continent, index);

            _logger.LogLine("Enter new value for Name or blank to Ignore");
            var name = Console.ReadLine();

            _logger.LogLine("Enter the New Value for Country");
            var userCountry = Console.ReadLine();

            while(NumberChecker.isNumber(name) || NumberChecker.isNumber(userCountry))
            {
                _logger.LogLine("No number, whitespace or letter allowed");

                _logger.LogLine("Enter new value for Name or blank to Ignore");
                name = Console.ReadLine();

                _logger.LogLine("Enter the New Value for Country");
                userCountry = Console.ReadLine();
            }

            model.Name = name;
            UserToUpdate.Name = model.Name;
            model.Country = userCountry;
            UserToUpdate.Country = model.Country;
        }


        private void New()
        {
            _menu.Display("Select the continent :\n1. Africa\n2. Europe\n3. Asia");

            switch (Console.ReadLine())
            {
                case "1":

                    CreateNewUser("Africa");
                    break;

                case "2":
                    CreateNewUser("Europe");
                    break;

                case "3":
                    CreateNewUser("Asia");
                    break;
                default:
                    _logger.Log("Invalid Input!\nTry Again!");
                    break;
            }

        }


        private void Update()
        {
            _menu.Display("Select the continent :\n1. Africa\n2. Europe\n3. Asia");

            switch (Console.ReadLine())
            {

                case "1":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchUsers(_userService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input))
                    {
                        UpdateUser("Africa", input);
                    }
                    else
                    {
                        _logger.Log("Invalid Input!\nTry Again!");
                    }
                    break;

                case "2":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchUsers(_userService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input1))
                    {
                        UpdateUser("Europe", input1);
                    }
                    else
                    {
                        _logger.Log("Invalid Input!\nTry Again!");
                    }
                    break;

                case "3":
                    _menu.Display("Select the country :\n Enter 0 - n in the order they appear");
                    FetchUsers(_userService.GetAll("Africa"));

                    if (int.TryParse(Console.ReadLine(), out var input2))
                    {
                        UpdateUser("Asia", input2);
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
            _menu.Display("Go to User Menu ?\n1. Yes\n2. Any other Key to go back main menu!");
        }

        private void FetchUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _logger.LogLine($"Id: {user.Id}\nName: {user.Name}\nRegion: {user.Country}\n");
            }
        }

        

    }
}

