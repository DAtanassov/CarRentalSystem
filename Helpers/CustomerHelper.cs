using CarRentalSystem.DB.CSV;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    public class CustomerHelper : IHelper<Customer>
    {
        // Object for read and write to database file
        private static readonly DBService<Customer> dbService = new DBService<Customer>(new CustomerDB());

        public List<Customer> GetItems() => dbService.GetList();

        public List<Customer> GetItems(int[] id)
        {
            List<Customer> items = GetItems();

            if (id.Length > 0)
                items = items.Where(x => id.Contains(x.ID)).ToList();

            return items;
        }
        
        public Customer? GetItemById(int id)
        {
            List<Customer> items = GetItems([id]);
            return GetItemById(items, id);
        }

        public Customer? GetItemById(List<Customer> items, int id)
        {
            if (items.Count == 0)
                return null;
            return items.FirstOrDefault(i => i.ID == id);
        }

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

        public bool AddItem() => AddEditItem(null);

        public bool EditItem(Customer item) => AddEditItem(item);

        public bool AddEditItem(Customer? item)
        {
            bool newItem = (item == null);
            bool cancel = false;

            if (item == null)
                item = new Customer("", "", ""); // Creat customer object if add new

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
                        string customerEmail = ""; // TODO - Validating
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCustomer e-mail address: ");
                            customerEmail = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(customerEmail))
                            {
                                Console.Write("\tE-mail address cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(customerEmail));
                        if (!string.IsNullOrEmpty(customerEmail) && item.Email != customerEmail)
                            item.Email = customerEmail;
                        break;
                    case "3":
                        string customerPhone = ""; // TODO - Validating
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCustomer phone number: ");
                            customerPhone = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(customerPhone))
                            {
                                Console.Write("\tPhone number cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(customerPhone));
                        if (!string.IsNullOrEmpty(customerPhone) && item.Phone != customerPhone)
                            item.Phone = customerPhone;
                        break;
                    case "4":
                        // TODO - Validating
                        running = false;
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
