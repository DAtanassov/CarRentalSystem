
using CarRentalSystem.Models.Interfaces;
using CarRentalSystem.Helpers;
using System.Text;

namespace CarRentalSystem.Models
{
    internal class Menu : IMenu
    {
        private static readonly MenuHelper menuHelper = new MenuHelper();
        private static readonly CarHelper carHelper = new CarHelper();

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.Title = MenuHelper.appName;

            MainMenu();
        }
        
        public void ExitMenu()
        {
            MenuHelper.PrintAppName();

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
                    case "1": // Cars menu
                        CarsMenu();
                        break;
                    case "2": // List Cars (available)
                        carHelper.PrintItems(true);
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "3": // Rent Car
                        RentACarMenu();
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "4": // Return Car
                        Car? car = carHelper.SelectItem(false);
                        if (car != null)
                        {
                            if (new RentalHelper().ReturnItem(car))
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe car was returned successfully.");
                            }
                            else
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe car was not returned.");
                            }
                        }
                        else
                        {
                            MenuHelper.PrintAppName();
                            Console.WriteLine("\tNo car selected for returning.");

                        }
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "5": // Customers menu (add/edit/remove)
                        CustomersMenu();
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

        private void CarsMenu()
        {
            Console.CursorVisible = false;
            menuHelper.PrintCarsMenuHeader();
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Dictionary<int, string[]> menu = menuHelper.GetCarsMenu();

            // variable for the selected car (edit/remove)
            Car? car = null;

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
                        if (carHelper.AddItem())
                            Console.WriteLine("\tThe car was created successfully.");
                        else
                            Console.WriteLine("\tThe car was not created.");
                        Console.WriteLine("\n\tPress any key to continue...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "2": // Edit Car
                        car = carHelper.SelectItem();
                        if (car != null)
                        {
                            if (carHelper.EditItem(car))
                                Console.WriteLine("\tThe car was modified successfully.");
                            else
                                Console.WriteLine("\tThe car was not modified.");
                            car = null;
                        }
                        else
                        {
                            MenuHelper.PrintAppName();
                            Console.WriteLine("\tNo car selected for editing.");
                        }
                        Console.WriteLine("\n\tPress any key to continue...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "3": // Remove Car
                        car = carHelper.SelectItem();
                        if (car != null)
                        {
                            if (carHelper.RemoveItem(car))
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe car was removed successfully.");
                            }
                            else
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe car was not removed.");

                            }
                            car = null;
                        }
                        else
                        {
                            MenuHelper.PrintAppName();
                            Console.WriteLine("\tNo car selected for removing.");
                        }
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "4": // List all Cars
                        carHelper.PrintItems(); 
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "5":
                        running = false;
                        break;
                    case "1000": // Exit
                        ExitMenu();
                        break;
                }
                Console.CursorVisible = false;
                menuHelper.PrintCarsMenuHeader();
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
            }
        }

        private void CustomersMenu()
        {
            CustomerHelper customerHelper = new CustomerHelper();

            Console.CursorVisible = false;
            menuHelper.PrintCustomersMenuHeader();
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Dictionary<int, string[]> menu = menuHelper.GetCustomersMenu();

            // variable for the selected customer (edit/remove)
            Customer? customer = null;

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
                    case "1": // Add Customer
                        if (customerHelper.AddItem())
                            Console.WriteLine("\tThe Customer was created successfully.");
                        else
                            Console.WriteLine("\tThe Cutomer was not created.");
                        Console.WriteLine("\n\tPress any key to continue...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "2": // Edit Customer
                        customer = customerHelper.SelectItem();
                        if (customer != null)
                        {
                            if (customerHelper.EditItem(customer))
                                Console.WriteLine("\tThe Customer was modified successfully.");
                            else
                                Console.WriteLine("\tThe Customer was not modified.");
                            customer = null;
                        }
                        else
                        {
                            MenuHelper.PrintAppName();
                            Console.WriteLine("\tNo Customer selected for editing.");
                        }
                        Console.WriteLine("\n\tPress any key to continue...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "3": // Remove Customer
                        customer = customerHelper.SelectItem();
                        if (customer != null)
                        {
                            if (customerHelper.RemoveItem(customer))
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe Customer was removed successfully.");
                            }
                            else
                            {
                                MenuHelper.PrintAppName();
                                Console.WriteLine("\tThe Customer was not removed.");

                            }
                            customer = null;
                        }
                        else
                        {
                            MenuHelper.PrintAppName();
                            Console.WriteLine("\tNo Customer selected for removing.");
                        }
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "4": // List all Customers
                        customerHelper.PrintItems();
                        Console.WriteLine("\n\tPress any key to return to the main menu...");
                        Console.ReadKey(); // Wait for user input before continuing
                        break;
                    case "5":
                        running = false;
                        break;
                    case "1000": // Exit
                        ExitMenu();
                        break;
                }
                Console.CursorVisible = false;
                menuHelper.PrintCustomersMenuHeader();
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
            }
        }

        private void RentACarMenu()
        {
            CustomerHelper customerHelper = new CustomerHelper();
            RentalHelper rentalHelper = new RentalHelper();

            Car? car = null;
            Customer? customer = null;
            DateTime startDate = DateTime.Today; // default date today
            DateTime endDate = startDate.AddDays(1); // default date start date + 1 day

            Console.CursorVisible = false;
            menuHelper.PrintRentACarHeader();
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Dictionary<int, string[]> menu = menuHelper.GetRentACarMenu(car, customer, startDate, endDate);

            bool cancel = false;
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

                switch (option)
                {
                    case "1":
                        car = carHelper.SelectItem(true);
                        break;
                    case "2":
                        customer = customerHelper.SelectItem();
                        break;
                    case "3":
                        Console.CursorVisible = true;
                        menuHelper.PrintRentACarHeader();
                        Console.Write("\n\tEnter start date: ");
                        while (!DateTime.TryParse(Console.ReadLine(), out startDate)
                            || startDate < DateTime.Today)
                        {
                            menuHelper.PrintRentACarHeader();
                            Console.WriteLine("\n\tStart date must be greater or equal than current!");
                            Console.WriteLine("\n\tPress \"Esc\" for cancel or any other key to continue...");
                            ConsoleKeyInfo userInput = Console.ReadKey();
                            if (userInput.Key == ConsoleKey.Escape)
                            { Console.CursorVisible = false;
                                break;
                            }

                            menuHelper.PrintRentACarHeader();
                            Console.Write("\n\tEnter start date: ");
                        }
                        Console.CursorVisible = false;
                        break;
                    case "4":
                        Console.CursorVisible = true;
                        menuHelper.PrintRentACarHeader();
                        Console.Write("\n\tEnter end date: ");
                        while (!DateTime.TryParse(Console.ReadLine(), out endDate)
                            || endDate <= startDate)
                        {
                            menuHelper.PrintRentACarHeader();
                            Console.WriteLine("\tEnd date must be greater than start date!");
                            Console.WriteLine("\n\tPress \"Esc\" for cancel or any other key to continue...");
                            ConsoleKeyInfo userInput = Console.ReadKey();
                            if (userInput.Key == ConsoleKey.Escape)
                            {
                                Console.CursorVisible = false;
                                break;
                            }
                            menuHelper.PrintRentACarHeader();
                            Console.Write("\n\tEnter end date: ");
                        }
                        Console.CursorVisible = false;
                        break;
                    case "5":
                        if (car != null && customer != null)
                            running = false;
                        else
                        {
                            menuHelper.PrintRentACarHeader();
                            Console.WriteLine("\n\tPlease fill in all the rental details!");
                            Console.WriteLine("\n\tPress any key to return to the main menu...");
                            Console.ReadKey(); // Wait for user input before continuing
                        }
                        break;
                    case "6":
                        cancel = true;
                        running = false;
                        break;
                }
                menuHelper.PrintRentACarHeader();
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();

                menu = menuHelper.GetRentACarMenu(car, customer, startDate, endDate);
            }

            if ((cancel || car == null || customer == null)
                || rentalHelper.AddItem(car, customer, startDate, endDate))
            {
                menuHelper.PrintRentACarHeader();
                Console.WriteLine("\n\tThe rent was not added..");
            }
            else
            { 
                menuHelper.PrintRentACarHeader();
                Console.WriteLine("\n\tThe rental was added successfully.");
            }
        }
    }
}
