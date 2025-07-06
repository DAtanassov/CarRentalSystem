using CarRentalSystem.DB.CSV;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    /// <summary>
    /// Helper class for managing customer objects in the car rental system.
    /// </summary>
    public class CustomerHelper : IHelper<Customer>
    {
        /// <summary>
        /// Object for read and write to database file
        /// </summary>
        private static readonly DBService<Customer> dbService = new DBService<Customer>(new CustomerDB());
        /// <summary>
        /// Instance of the Validator class for validating customer objects and their properties.
        /// </summary>
        private static readonly Validator validator = new Validator();

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetItems() => dbService.GetList();
        /// <summary>
        /// Retrieves items by array of IDs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Customer> GetItems(int[] id)
        {
            List<Customer> items = GetItems();

            if (id.Length > 0)
                items = items.Where(x => id.Contains(x.ID)).ToList();

            return items;
        }
        /// <summary>
        /// Retrieves a single item by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer? GetItemById(int id)
        {
            List<Customer> items = GetItems([id]);
            return GetItemById(items, id);
        }
        /// <summary>
        /// Retrieves an item with the specified ID from a given list.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer? GetItemById(List<Customer> items, int id)
        {
            if (items.Count == 0)
                return null;
            return items.FirstOrDefault(i => i.ID == id);
        }
        /// <summary>
        /// Displays all items in a user-friendly format.
        /// </summary>
        public void PrintItems()
        {
            MenuHelper.PrintAppName();
            Console.WriteLine("\t\tCustomers\n");

            List<Customer> items = GetItems();
            items = items.Where(x => !x.IsDeleted).ToList();

            int counter = 0;
            foreach (Customer item in items)
                Console.WriteLine($"\t{++counter}. {item.ExtendedInfo()}");

        }
        /// <summary>
        /// Select an item from a data source
        /// </summary>
        /// <returns></returns>
        public Customer? SelectItem()
        {
            List<Customer> items = GetItems();
            items = items.Where(x => !x.IsDeleted).ToList();

            if (items.Count == 0)
                return null;

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();
            MenuHelper.PrintAppName();
            Console.WriteLine("\t\tSelect customer\n");

            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Func<string[], string[]> iName = (string[] n) => n;
            Dictionary<int, string[]> menu = items.Select((val, index) => new { Index = index, Value = val })
                                                    .ToDictionary(h => h.Index, h => iName([h.Value.Info(), h.Value.ID.ToString()]));
            menu.Add(menu.Count, ["Cancel", "0"]);
            for (int i = menu.Count; i > 0; i--)
            {
                menu.Add(i, menu[i - 1]);
                menu.Remove(i - 1);
            }

            Customer? item = null;

            bool running = true;
            while (running)
            {
                Console.SetCursorPosition(menuParams.left, menuParams.top);

                menuHelper.PrintMenuElements(menu, menuParams);

                menuParams.key = Console.ReadKey(false);

                switch (menuParams.key.Key)
                {
                    case ConsoleKey.UpArrow:
                        menuParams.choice = menuParams.choice == 1 ? menu.Count : menuParams.choice - 1;
                        continue;

                    case ConsoleKey.DownArrow:
                        menuParams.choice = menuParams.choice == menu.Count ? 1 : menuParams.choice + 1;
                        continue;

                    case ConsoleKey.Enter:
                        if (menuParams.choice != menu.Count)
                            item = GetItemById(items, int.Parse(menu[menuParams.choice][1]));
                        running = false;
                        break;
                }
            }

            return item;
        }
        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <returns></returns>
        public bool AddItem() => AddEditItem(null);
        /// <summary>
        /// Edit an existing item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool EditItem(Customer item) => AddEditItem(item);
        /// <summary>
        /// Implementation of adding or editing an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddEditItem(Customer? item)
        {
            bool newItem = (item == null);
            bool cancel = false;

            if (item == null)
                item = new Customer("", "", ""); // Creat customer object if add new

            List<Customer> items = GetItems();

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();

            menuHelper.PrintAddEditCustomerMenuHeader(newItem);

            Dictionary<int, string[]> menu = menuHelper.GetAddEditCustomerMenu(item);
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();
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
                    case "1":
                        string customerName = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCustomer name: ");
                            customerName = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(customerName))
                            {
                                Console.Write("\tCustomer name cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(customerName));
                        if (!string.IsNullOrEmpty(customerName) && item.Name != customerName)
                            item.Name = customerName;
                        break;
                    case "2":
                        string customerEmail = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCustomer e-mail address: ");
                            customerEmail = Console.ReadLine() ?? string.Empty;

                            if (!validator.EmailValidate(customerEmail, item.ID, dbService, items))
                            {
                                if (string.IsNullOrEmpty(customerEmail))
                                    Console.WriteLine("\tEmail address cannot be empty!");
                                else
                                    Console.WriteLine("\tEmail address is used or not valid!");

                                Console.Write("\tCancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (!validator.EmailValidate(customerEmail, item.ID, dbService, items));
                        if (validator.EmailValidate(customerEmail, item.ID, dbService, items))
                            item.Email = customerEmail;
                        break;
                    case "3":
                        string customerPhone = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCustomer phone number: ");
                            (menuParams.left, menuParams.top) = Console.GetCursorPosition();
                            Console.WriteLine("\n\t(123-456-7890, (123) 456-7890, 1234567890)");
                            Console.SetCursorPosition(menuParams.left, menuParams.top);

                            customerPhone = Console.ReadLine() ?? string.Empty;

                            if (!validator.PhoneNumberValidate(customerPhone, item.ID, dbService, items))
                            {
                                Console.Write("\tPhone number is not valid! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (!validator.PhoneNumberValidate(customerPhone, item.ID, dbService, items));
                        if (validator.PhoneNumberValidate(customerPhone, item.ID, dbService, items))
                            item.Phone = customerPhone;
                        break;
                    case "4":
                        if (validator.CustomerValidate(item))
                            running = false;
                        else
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.WriteLine("\n\tName, E-mail and Phone number are required!");
                            Console.WriteLine("\n\tPress any key to return to the main menu...");
                            Console.ReadKey(); // Wait for user input before continuing
                        }
                        break;
                    case "5": // Cancel
                        cancel = true;
                        running = false;
                        break;
                }

                Console.CursorVisible = false;
                menuHelper.PrintAddEditCustomerMenuHeader(newItem);
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
                menu = menuHelper.GetAddEditCustomerMenu(item);
            }


            if (cancel)
                return false;
            else
            {
                if (newItem)
                    dbService.Insert(item);
                else
                    dbService.Update(item);
            }

            return true;

        }
        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveItem(Customer item)
        {
            string title = "Customer";

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();
            menuHelper.PrintRemoveItemHeader(title);
            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Dictionary<int, string[]> menu = menuHelper.GetRemoveItemMenu();

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

                Console.CursorVisible = true;
                switch (option)
                {
                    case "1":
                        menuHelper.PrintRemoveItemHeader(title);
                        Console.Write($"\tAll data will be removed! Continue? (\"Y/n\"): ");
                        if ((Console.ReadLine() ?? "n").ToLower() == "y")
                        {
                            dbService.Delete(item);
                            running = false;
                        }
                        break;
                    case "2":
                        menuHelper.PrintRemoveItemHeader(title);
                        Console.Write($"\tMark as deleted? (\"Y/n\"): ");
                        if ((Console.ReadLine() ?? "n").ToLower() == "y")
                        {
                            item.IsDeleted = true;
                            dbService.Update(item);
                            running = false;
                        }
                        break;
                    case "3":
                        cancel = true;
                        running = false;
                        break;
                }
                Console.CursorVisible = false;
                menuHelper.PrintRemoveItemHeader(title);
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
            }

            return !cancel;
        }
    }
}
