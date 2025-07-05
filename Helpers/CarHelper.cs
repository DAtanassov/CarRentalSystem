using CarRentalSystem.DB.CSV;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    public class CarHelper : IHelper<Car>
    {
        // Object for read and write to database file
        private static readonly DBService<Car> dbService = new DBService<Car>(new CarDB());

        public List<Car> GetItems() => dbService.GetList();

        public List<Car> GetItems(int[] id)
        {
            List<Car> items = GetItems();

            if (id.Length > 0)
                items = items.Where(x => id.Contains(x.ID)).ToList();

            return items;
        }

        public Car? GetItemById(int id)
        {
            List<Car> items = GetItems([id]);
            return GetItemById(items, id);
        }

        public Car? GetItemById(List<Car> items, int id)
        {
            if (items.Count == 0)
                return null;
            return items.FirstOrDefault(i => i.ID == id);
        }

        public void PrintItems() => PrintItems(null);

        public void PrintItems(bool? available)
        {
            MenuHelper.PrintAppName();
            if (available == null)
                Console.WriteLine("\t\tCars\n");
            else
                Console.WriteLine($"\t\tCars ({(available == true ? "Available" : "Rented")})\n");

            List<Car> items = GetItems();
            if (available == null)
                items = items.Where(x => !x.IsDeleted).ToList();
            else
                items = items.Where(x => !x.IsDeleted && x.Availability == available).ToList();

            int counter = 0;
            foreach (Car item in items)
                Console.WriteLine($"\t{++counter}. {item.ExtendedInfo()}");
        }

        public Car? SelectItem() => SelectItem(null);

        public Car? SelectItem(bool? available)
        {
            List<Car> items = GetItems();
            if (available == null)
                items = items.Where(x => !x.IsDeleted).ToList();
            else
                items = items.Where(x => !x.IsDeleted && x.Availability == available).ToList();


            if (items.Count == 0)
                return null;

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();
            MenuHelper.PrintAppName();
            Console.WriteLine("\t\tSelect car\n");

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

            Car? item = null;

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

        public bool EditItem(Car item) => AddEditItem(item);

        public bool AddEditItem(Car? item)
        {
            bool newItem = (item == null);
            bool cancel = false;

            if (item == null)
                item = new Car("", "", ""); // Creat car object if add new

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();

            menuHelper.PrintAddEditCarMenuHeader(newItem);

            Dictionary<int, string[]> menu = menuHelper.GetAddEditCarMenu(item, newItem);
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
                        string carMake = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCar manufacturerr: ");
                            carMake = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(carMake))
                            {
                                Console.Write("\tCar manufacturerr cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(carMake));
                        if (!string.IsNullOrEmpty(carMake) && item.Make != carMake)
                            item.Make = carMake;
                        break;
                    case "2":
                        string carModel = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCar model: ");
                            carModel = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(carModel))
                            {
                                Console.Write("\tCar model cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(carModel));
                        if (!string.IsNullOrEmpty(carModel) && item.Model != carModel)
                            item.Model = carModel;
                        break;
                    case "3":
                        int carYear = 0;
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCar year: ");
                            if (!int.TryParse(Console.ReadLine() ?? string.Empty, out carYear))
                                                        {
                                Console.Write("\tCar year cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }
                        } while (carYear <= 0);
                        if (carYear > 0 && item.Year != carYear)
                            item.Year = carYear;
                        break;
                    case "4":
                        string carType = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.Write("\tCar type: ");
                            carType = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(carType))
                            {
                                Console.Write("\tCar type cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(carType));
                        if (!string.IsNullOrEmpty(carType) && item.CarType != carType)
                            item.CarType = carType;
                        break;
                    case "5":
                        item.Availability = !item.Availability;
                        break;
                    case "6":
                        if (!string.IsNullOrEmpty(item.Make)
                            && !string.IsNullOrEmpty(item.Model)
                            && !string.IsNullOrEmpty(item.CarType)
                            && item.Year > 0)
                            running = false;
                        else
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newItem);
                            Console.WriteLine("\n\tMake, Model, Year and Type are required!");
                            Console.WriteLine("\n\tPress any key to return to the main menu...");
                            Console.ReadKey(); // Wait for user input before continuing
                        }
                        break;
                    case "7": // Cancel
                        cancel = true;
                        running = false;
                        break;
                }

                Console.CursorVisible = false;
                menuHelper.PrintAddEditCarMenuHeader(newItem);
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
                menu = menuHelper.GetAddEditCarMenu(item, newItem);

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
    
        public bool RemoveItem(Car item)
        {
            string title = "Car";

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
    
        public bool ChangeAlailability(Car car, bool available)
        {
            car.Availability = available;
            
            dbService.Update(car);

            return true;
        }
    }
}
