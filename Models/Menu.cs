
using CarRentalSystem.Models.Interfaces;
using CarRentalSystem.Helpers;
using System.Text;

namespace CarRentalSystem.Models
{
    internal class Menu : IMenu
    {
        private static readonly MenuHelper menuHelper = new MenuHelper();

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.Title = MenuHelper.appName;

            MainMenu();
        }
        public void ExitMenu()
        {
            menuHelper.PrintAppName();

            Console.CursorVisible = true;
            Console.Write("\tExit application? [Y/n]: ");
            if ((Console.ReadLine() ?? "n").ToLower() == "y")
            {
                Console.Clear();
                Environment.Exit(0);
            }
        }

        private void MainMenu()
        {
            Console.CursorVisible = false;
            menuHelper.PrintMainMenuHeader();
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Dictionary<int, string[]> menu = menuHelper.GetMainMenu();


            bool running = true;

            while (running)
            {
                Console.SetCursorPosition(menuParams.left, menuParams.top);

                menuHelper.PrintMenuElements(menu, menuParams);

                menuParams.key = Console.ReadKey(false);

                string option = string.Empty;

                switch (menuParams.key.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuParams.choice = menuParams.choice == 1 ? menu.Count : menuParams.choice - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        menuParams.choice = menuParams.choice == menu.Count ? 1 : menuParams.choice + 1;
                        break;

                    case ConsoleKey.Enter:
                        option = menu[menuParams.choice][1];
                        break;
                }

                if (string.IsNullOrEmpty(option) && menuParams.key.Key != ConsoleKey.Enter)
                    continue; // Skip if no option selected

                Console.CursorVisible = true;
                switch (option)
                {
                    case "1": // Add Car
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "2": // Edit Car
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "3": // Remove Car
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "4": // List Cars
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "5": // Rent Car
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "6": // Return Car
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "1000": // Exit
                        ExitMenu();
                        break;
                }
                Console.CursorVisible = false;
                menuHelper.PrintMainMenuHeader();
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
            }

        }
    }
}
