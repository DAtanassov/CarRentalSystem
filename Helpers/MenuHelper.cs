
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    public class MenuHelper
    {
        // Static readonly field for the application name
        public static readonly string appName = "Car Rental System";

        public struct MenuParams
        {
            // Properties to hold the menu parameters
            public int left { get; set; } // cursor left position
            public int top { get; set; } // cursor top position
            public int choice { get; set; } // line position of cursor 
            public string prefix { get; } = "\u001b[32m• "; // prefix for marked line
            public ConsoleKeyInfo key { get; set; }
            public MenuParams(int _choice)
            {
                left = 0;
                top = 0;
                choice = _choice;
            }
        }

        public static void PrintAppName(bool clear = true)
        {
            if (clear)
                Console.Clear();
            // Set the console title
            Console.WriteLine($"\t\t\u001b[1m{appName}\u001b[0m\n");
        }

        private void PrintMenuHeader(string header, bool printName)
        {
            if (printName)
                PrintAppName();
            // Print menu header
            Console.WriteLine($"\t\t\u001b[1m{header}\u001b[0m\n");
        }

        public void PrintMenuElements(Dictionary<int, string[]> menu, MenuParams menuParams)
        {
            // Print the menu elements
            foreach (var element in menu)
            {
                Console.WriteLine($"\t{(menuParams.choice == element.Key ? menuParams.prefix : "  ")}" +
                                    $"{element.Key}. {element.Value[0]}\u001b[0m");
            }

        }

        public void PrintMainMenuHeader(bool printName = true)
            => PrintMenuHeader("Main Menu", printName);

        public Dictionary<int, string[]> GetMainMenu()
        {
            // Create a dictionary to hold the Main menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, ["Cars menu", "1"]);
            menu.Add(menu.Count + 1, ["List Cars (available)", "2"]);
            menu.Add(menu.Count + 1, ["Rent Car", "3"]);
            menu.Add(menu.Count + 1, ["Return Car", "4"]);
            menu.Add(menu.Count + 1, ["Customers menu", "5"]);
            menu.Add(menu.Count + 1, ["Exit", "1000"]);

            return menu;
        }

        public void PrintCarsMenuHeader(bool printName = true)
            => PrintMenuHeader("Cars Menu", printName);

        public Dictionary<int, string[]> GetCarsMenu()
        {
            // Create a dictionary to hold the Customers menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, ["Add Car", "1"]);
            menu.Add(menu.Count + 1, ["Edit Car", "2"]);
            menu.Add(menu.Count + 1, ["Remove Car", "3"]);
            menu.Add(menu.Count + 1, ["List all Cars", "4"]);
            menu.Add(menu.Count + 1, ["< Back", "5"]);
            menu.Add(menu.Count + 1, ["Exit", "1000"]);

            return menu;
        }

        public void PrintCustomersMenuHeader(bool printName = true)
            => PrintMenuHeader("Customers Menu", printName);

        public Dictionary<int, string[]> GetCustomersMenu()
        {
            // Create a dictionary to hold the Customers menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, ["Add Customer", "1"]);
            menu.Add(menu.Count + 1, ["Edit Customer", "2"]);
            menu.Add(menu.Count + 1, ["Remove Customer", "3"]);
            menu.Add(menu.Count + 1, ["List Customers", "4"]);
            menu.Add(menu.Count + 1, ["< Back", "5"]);
            menu.Add(menu.Count + 1, ["Exit", "1000"]);

            return menu;
        }

        public void PrintAddEditCarMenuHeader(bool newItem, bool printName = true)
            => PrintMenuHeader($"{(newItem ? "Create" : "Edit")} Car", printName);

        public Dictionary<int, string[]> GetAddEditCarMenu(Car car, bool newItem)
        {
            // Create a dictionary to hold the car add/edit menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, [$"Make: {car.Make}", "1"]);
            menu.Add(menu.Count + 1, [$"Model: {car.Model}", "2"]);
            menu.Add(menu.Count + 1, [$"Year: {car.Year}", "3"]);
            menu.Add(menu.Count + 1, [$"Type: {car.CarType}", "4"]);
            if (!newItem) // if a new car is aways Available
                menu.Add(menu.Count + 1, [$"Availability: {(car.Availability ? "Available" : "Rented")}", "5"]);
            menu.Add(menu.Count + 1, ["Save", "6"]);
            menu.Add(menu.Count + 1, ["Cancel", "7"]);

            return menu;
        }

        public void PrintAddEditCustomerMenuHeader(bool newItem, bool printName = true)
            => PrintMenuHeader($"{(newItem ? "Create" : "Edit")} Customer", printName);

        public Dictionary<int, string[]> GetAddEditCustomerMenu(Customer customer)
        {
            // Create a dictionary to hold the car add/edit menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, [$"Name: {customer.Name}", "1"]);
            menu.Add(menu.Count + 1, [$"E-mail: {customer.Email}", "2"]);
            menu.Add(menu.Count + 1, [$"Phone: {customer.Phone}", "3"]);
            menu.Add(menu.Count + 1, ["Save", "4"]);
            menu.Add(menu.Count + 1, ["Cancel", "5"]);

            return menu;
        }
    }
}
