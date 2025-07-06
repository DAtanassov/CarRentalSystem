using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    /// <summary>
    /// Helper class for managing console menus in the Car Rental System application.
    /// </summary>
    public class MenuHelper
    {
        /// <summary>
        /// Static readonly field for the application name
        /// </summary>
        public static readonly string appName = "Car Rental System";

        /// <summary>
        /// Structure to hold menu parameters.
        /// </summary>
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

        /// <summary>
        /// Prints the application name in the console.
        /// </summary>
        /// <param name="clear"></param>
        public static void PrintAppName(bool clear = true)
        {
            if (clear)
                Console.Clear();
            // Set the console title
            Console.WriteLine($"\t\t\u001b[1m{appName}\u001b[0m\n");
        }

        /// <summary>
        /// Prints the menu header with the specified header text and an option to print the application name.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="printName"></param>
        private void PrintMenuHeader(string header, bool printName)
        {
            if (printName)
                PrintAppName();
            // Print menu header
            Console.WriteLine($"\t\t\u001b[1m{header}\u001b[0m\n");
        }

        /// <summary>
        /// Prints the menu elements based on the provided dictionary and menu parameters.
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="menuParams"></param>
        public void PrintMenuElements(Dictionary<int, string[]> menu, MenuParams menuParams)
        {
            // Print the menu elements
            foreach (var element in menu)
            {
                Console.WriteLine($"\t{(menuParams.choice == element.Key ? menuParams.prefix : "  ")}" +
                                    $"{element.Key}. {element.Value[0]}\u001b[0m");
            }

        }

        /// <summary>
        /// Prints the main menu header with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintMainMenuHeader(bool printName = true)
            => PrintMenuHeader("Main Menu", printName);

        /// <summary>
        /// Defines the main menu items and returns them as a dictionary.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Prints the Cars menu header with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintCarsMenuHeader(bool printName = true)
            => PrintMenuHeader("Cars Menu", printName);

        /// <summary>
        /// Defines the Cars menu items and returns them as a dictionary.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Prints the Customers menu header with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintCustomersMenuHeader(bool printName = true)
            => PrintMenuHeader("Customers Menu", printName);

        /// <summary>
        /// Defines the Customers menu items and returns them as a dictionary.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Prints the header for adding or editing a car, with an option to print the application name.
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="printName"></param>
        public void PrintAddEditCarMenuHeader(bool newItem, bool printName = true)
            => PrintMenuHeader($"{(newItem ? "Create" : "Edit")} Car", printName);

        /// <summary>
        /// Defines the menu items for adding or editing a car and returns them as a dictionary.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Prints the header for adding or editing a customer, with an option to print the application name.
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="printName"></param>
        public void PrintAddEditCustomerMenuHeader(bool newItem, bool printName = true)
            => PrintMenuHeader($"{(newItem ? "Create" : "Edit")} Customer", printName);

        /// <summary>
        /// Defines the menu items for adding or editing a customer and returns them as a dictionary.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Prints the header for removing an item, with an option to print the application name.
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="printName"></param>
        public void PrintRemoveItemHeader(string itemName, bool printName = true)
            => PrintMenuHeader($"Remove {itemName}", printName);

        /// <summary>
        /// Defines the menu items for removing an item and returns them as a dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string[]> GetRemoveItemMenu()
        {
            // Create a dictionary to hold the Main menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, ["Delete (remove all data)", "1"]);
            menu.Add(menu.Count + 1, ["Remove (mark as deleted)", "2"]);
            menu.Add(menu.Count + 1, ["Cancel", "3"]);

            return menu;
        }

        /// <summary>
        /// Prints the header for the Rent A Car menu, with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintRentACarHeader(bool printName = true)
            => PrintMenuHeader("Rent A Car", printName);

        /// <summary>
        /// Defines the menu items for renting a car and returns them as a dictionary.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="customer"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Dictionary<int, string[]> GetRentACarMenu(Car? car, Customer? customer, DateTime? startDate, DateTime? endDate)
        {
            // Create a dictionary to hold the Main menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, [$"Car: {((car == null) ? "<select>" : car.Info())}", "1"]);
            menu.Add(menu.Count + 1, [$"Customer: {((customer == null) ? "<select>" : customer.Info())}", "2"]);
            menu.Add(menu.Count + 1, [$"Start date: {((startDate == null) ? "<select>" : ((DateTime)startDate).ToShortDateString())}", "3"]);
            menu.Add(menu.Count + 1, [$"End date: {((endDate == null) ? "<select>" : ((DateTime)endDate).ToShortDateString())}", "4"]);
            menu.Add(menu.Count + 1, ["Rent", "5"]);
            menu.Add(menu.Count + 1, ["Cancel", "6"]);

            return menu;
        }

        /// <summary>
        /// Prints the header for the Search Car menu, with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintSearchItemHeader(bool printName = true)
            => PrintMenuHeader("Search Car", printName);

        /// <summary>
        /// Defines the menu items for searching a car and returns them as a dictionary.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<int, string[]> GetSearchFilterMenu(string make, string model, bool status, int id)
        {
            // Create a dictionary to hold the Main menu items
            Dictionary<int, string[]> menu = new Dictionary<int, string[]>();

            menu.Add(menu.Count + 1, [$"Make: {make}", "1"]);
            menu.Add(menu.Count + 1, [$"Model: {model}", "2"]);
            menu.Add(menu.Count + 1, [$"Status: {(status ? "Available" : "Rented")}", "3"]);
            menu.Add(menu.Count + 1, [$"ID: {id}", "4"]);
            menu.Add(menu.Count + 1, ["Search", "5"]);
            menu.Add(menu.Count + 1, ["Cancel", "6"]);

            return menu;
        }

        /// <summary>
        /// Defines the menu items for selecting a car from a list of search results and returns them as a dictionary.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Dictionary<int, string[]> GetSearchItemMenu(List<Car> items)
        {
            // Create a dictionary to hold the Main menu items
            Func<string[], string[]> iName = (string[] n) => n;
            Dictionary<int, string[]> menu = items.Select((val, index) => new { Index = index, Value = val })
                                                    .ToDictionary(h => h.Index, h => iName([h.Value.Info(), h.Value.ID.ToString()]));
            menu.Add(menu.Count, ["Cancel", "0"]);
            for (int i = menu.Count; i > 0; i--)
            {
                menu.Add(i, menu[i - 1]);
                menu.Remove(i - 1);
            }

            return menu;
        }

        /// <summary>
        /// Prints the header for selecting a car, with an option to print the application name.
        /// </summary>
        /// <param name="printName"></param>
        public void PrintSelectCarHeader(bool printName = true)
            => PrintMenuHeader("Select Car", printName);
    }
}
