﻿using CarRentalSystem.DB.CSV;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    /// <summary>
    /// Helper class for managing car objects in the car rental system.
    /// </summary>
    public class CarHelper : IHelper<Car>, ISearchable
    {
        /// <summary>
        /// Object for read and write to database file
        /// </summary>
        private static readonly DBService<Car> dbService = new DBService<Car>(new CarDB());
        /// <summary>
        /// Instance of the Validator class for validating car objects.
        /// </summary>
        private static readonly Validator validator = new Validator();

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns></returns>
        public List<Car> GetItems() => dbService.GetList();
        /// <summary>
        /// Retrieves items by array of IDs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Car> GetItems(int[] id)
        {
            List<Car> items = GetItems();

            if (id.Length > 0)
                items = items.Where(x => id.Contains(x.ID)).ToList();

            return items;
        }
        /// <summary>
        /// Retrieves a single item by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car? GetItemById(int id)
        {
            List<Car> items = GetItems([id]);
            return GetItemById(items, id);
        }
        /// <summary>
        /// Retrieves an item with the specified ID from a given list.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car? GetItemById(List<Car> items, int id)
        {
            if (items.Count == 0)
                return null;
            return items.FirstOrDefault(i => i.ID == id);
        }
        /// <summary>
        /// Searches for a car in a list of cars based on user input.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Car? SearchCars(List<Car>? items)
        {
            if (items == null)
                items = GetItems();
            items = items.Where(x => !x.IsDeleted).ToList();

            Car? car = null;
            bool cancel = false;
            string make = string.Empty;
            string model = string.Empty;
            int id = 0;
            bool status = true; // true - available, false - rented

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();

            menuHelper.PrintSearchItemHeader();

            Dictionary<int, string[]> menu = menuHelper.GetSearchFilterMenu(make, model, status, id);

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
                    case "1": // Make
                        menuHelper.PrintSearchItemHeader();
                        Console.Write("\tCar manufacturer: ");
                        make = Console.ReadLine() ?? string.Empty;
                        if (string.IsNullOrEmpty(make))
                            make = string.Empty;
                        break;
                    case "2": // Model
                        menuHelper.PrintSearchItemHeader();
                        Console.Write("\tCar model: ");
                        model = Console.ReadLine() ?? string.Empty;
                        break;
                    case "3": // Status
                        menuHelper.PrintSearchItemHeader();
                        Console.Write("\tCar status [available/rented]: ");
                        string statusInput = Console.ReadLine() ?? string.Empty;
                        if (statusInput.ToLower() == "available")
                            status = true;
                        else if (statusInput.ToLower() == "rented")
                            status = false;
                        else
                            status = true; // Default to available if input is invalid
                        break;
                    case "4": // ID
                        menuHelper.PrintSearchItemHeader();
                        Console.Write("\tCar ID: ");
                        if (int.TryParse(Console.ReadLine(), out int parsedId))
                            id = parsedId;
                        else
                            id = 0;
                        break;
                    case "5": // Search
                        running = false;
                        break;
                    case "6": // Cancel
                        cancel = true;
                        running = false;
                        break;
                }

                Console.CursorVisible = false;
                menuHelper.PrintSearchItemHeader();
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
                menu = menuHelper.GetSearchFilterMenu(make, model, status, id);
            }

            if (cancel)
                return null;

            if (id > 0)
                return GetItemById(items, id);
            else
            {
                
                if (!string.IsNullOrEmpty(make))
                    items = items.Where(x => x.Make.Contains(make, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!string.IsNullOrEmpty(model))
                    items = items.Where(x => x.Model.Contains(model, StringComparison.OrdinalIgnoreCase)).ToList();
                if (status)
                    items = items.Where(x => x.Availability).ToList();
                else
                    items = items.Where(x => !x.Availability).ToList();
                if (items.Count == 0)
                {
                    Console.WriteLine("\n\tNo cars found with the specified criteria.");
                    Console.WriteLine("\n\tPress any key to return to the main menu...");
                    Console.ReadKey(); // Wait for user input before continuing
                    return null;
                }
            }

            menuHelper.PrintSelectCarHeader();
            running = true;
            menuParams.choice = 1;
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            menu = menuHelper.GetSearchItemMenu(items);

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
                            car = GetItemById(items, int.Parse(menu[menuParams.choice][1]));
                        running = false;
                        break;
                }
            }

            return car;
        }
        /// <summary>
        /// Displays all items in a user-friendly format.
        /// </summary>
        public void PrintItems() => PrintItems(null);
        /// <summary>
        /// Displays all items in a user-friendly format.
        /// </summary>
        /// <param name="available"></param>
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
        /// <summary>
        /// Select an item from a data source
        /// </summary>
        /// <returns></returns>
        public Car? SelectItem() => SelectItem(null);
        /// <summary>
        /// Select an item from a data source
        /// </summary>
        /// <param name="available"></param>
        /// <returns></returns>
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
            menuHelper.PrintSelectCarHeader();

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
        public bool EditItem(Car item) => AddEditItem(item);
        /// <summary>
        /// Implementation of adding or editing an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
                        if (validator.CarValidate(item))
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
        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Change availability of a car.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        public bool ChangeAvailability(Car car, bool available)
        {
            car.Availability = available;
            
            dbService.Update(car);

            return true;
        }
    }
}
