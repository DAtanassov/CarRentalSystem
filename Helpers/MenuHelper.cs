
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
        {
            if (printName)
                PrintAppName();
            // Print the main menu header
            Console.WriteLine("\n\t\t\u001b[1mMain Menu\u001b[0m\n");
        }

        public Dictionary<int, string[]> GetMainMenu()
        {
            // Create a dictionary to hold the Main menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, ["Add Car", "1"]);
            menu.Add(menu.Count + 1, ["Edit Car", "2"]);
            menu.Add(menu.Count + 1, ["Remove Car", "3"]);
            menu.Add(menu.Count + 1, ["List Cars", "4"]);
            menu.Add(menu.Count + 1, ["Rent Car", "5"]);
            menu.Add(menu.Count + 1, ["Return Car", "6"]);
            menu.Add(menu.Count + 1, ["Exit", "1000"]);

            return menu;
        }

        public void PrintAddEditCarMenuHeader(bool newCar, bool printName = true)
        {
            if (printName)
                PrintAppName();
            // Print the add/edit menu header
            Console.WriteLine($"\t\t\u001b[1m{(newCar ? "Create" : "Edit")} Car\u001b[0m\n");
        }

        public Dictionary<int, string[]> GetAddEditCarMenu(Car car, bool newCar)
        {
            // Create a dictionary to hold the car add/edit menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, [$"Make: {car.Make}", "1"]);
            menu.Add(menu.Count + 1, [$"Model: {car.Model}", "2"]);
            menu.Add(menu.Count + 1, [$"Year: {car.Year}", "3"]);
            menu.Add(menu.Count + 1, [$"Type: {car.CarType}", "4"]);
            if (!newCar) // if a new car is aways Available
                menu.Add(menu.Count + 1, [$"Availability: {(car.Availability ? "Available" : "Rented")}", "5"]);
            menu.Add(menu.Count + 1, ["Save", "6"]);
            menu.Add(menu.Count + 1, ["Cancel", "7"]);

            return menu;
        }
    }
}
