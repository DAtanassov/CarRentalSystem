using CarRentalSystem.DB.CSV;
using CarRentalSystem.Models;
using System.Collections.Generic;

namespace CarRentalSystem.Helpers
{
    public class CarHelper
    {
        // Object for read and write to database file
        private static readonly DBService<Car> dbService = new DBService<Car>(new CarDB());

        public static List<Car> GetCars() => dbService.GetList();
        public static List<Car> GetCars(int[] id)
        {
            List<Car> items = GetCars();

            if (id.Length > 0)
                items = items.Where(x => id.Contains(x.ID)).ToList();

            return items;
        }
        public Car? GetCarById(int id)
        {
            List<Car> items = GetCars([id]);
            return GetCarById(items, id);
        }

        public Car? GetCarById(List<Car> items, int id)
        {
            if (items.Count == 0)
                return null;
            return items.FirstOrDefault(i => i.ID == id);
        }

        public Car? SelectCar()
        {
            List<Car> cars = GetCars();

            if (cars.Count == 0)
                return null;

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();
            MenuHelper.PrintAppName();
            Console.WriteLine("\t\tSelect car\n");

            var menuParams = new MenuHelper.MenuParams(1);
            (menuParams.left, menuParams.top) = Console.GetCursorPosition();

            Func<string[], string[]> iName = (string[] n) => n;
            Dictionary<int, string[]> menu = cars.Select((val, index) => new { Index = index, Value = val })
                                                    .ToDictionary(h => h.Index, h => iName([h.Value.Info(), h.Value.ID.ToString()]));
            menu.Add(menu.Count, ["Cancel", "0"]);
            for (int i = menu.Count; i > 0; i--)
            {
                menu.Add(i, menu[i - 1]);
                menu.Remove(i - 1);
            }

            Car? car = null;

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
                            car = GetCarById(cars, int.Parse(menu[menuParams.choice][1]));
                        running = false;
                        break;
                }
            }

            return car;
        }

        public bool AddCar() => AddEditCar();
        public bool EditCar(Car car) => AddEditCar(car);

        private bool AddEditCar(Car? car = null)
        {
            bool newCar = (car == null);
            bool cancel = false;

            if (newCar)
                car = new Car("", "", ""); // Creat car object if add new

            List<Car> cars = GetCars();

            Console.CursorVisible = false;
            MenuHelper menuHelper = new MenuHelper();

            menuHelper.PrintAddEditCarMenuHeader(newCar);

            Dictionary<int, string[]> menu = menuHelper.GetAddEditCarMenu(car, newCar);
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
                        menuHelper.PrintAddEditCarMenuHeader(newCar);
                        Console.Write("\tCar manufacturerr: ");
                        string carMake = Console.ReadLine() ?? string.Empty;
                        while (string.IsNullOrEmpty(carMake))
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newCar);
                            Console.WriteLine("\tCar manufacturerr cannot be empty! Cancel input? (\"Y/n\"): ");
                            if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                break;
                            else
                            {
                                menuHelper.PrintAddEditCarMenuHeader(newCar);
                                Console.Write("\tCar manufacturerr: ");
                                carMake = Console.ReadLine() ?? string.Empty;
                            }
                        }
                        car.Make = carMake;
                        break;
                    case "2":
                        string carModel = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newCar);
                            Console.Write("\tCar model: ");
                            carModel = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(carModel))
                            {
                                Console.WriteLine("\tCar model cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(carModel));
                        car.Model = carModel;
                        break;
                    case "3":
                        int carYear = 0;
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newCar);
                            Console.Write("\tCar year: ");
                            if (!int.TryParse(Console.ReadLine() ?? string.Empty, out carYear))
                                                        {
                                Console.WriteLine("\tCar year cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }
                        } while (carYear <= 0);
                        car.Year = carYear;
                        break;
                    case "4":
                        string carType = "";
                        do
                        {
                            menuHelper.PrintAddEditCarMenuHeader(newCar);
                            Console.Write("\tCar type: ");
                            carType = Console.ReadLine() ?? string.Empty;

                            if (string.IsNullOrEmpty(carType))
                            {
                                Console.WriteLine("\tCar type cannot be empty! Cancel input? (\"Y/n\"): ");
                                if ((Console.ReadLine() ?? "n").ToLower() == "y")
                                    break;
                            }

                        } while (string.IsNullOrEmpty(carType));
                        car.CarType = carType;
                        break;
                    case "5":
                        car.Availability = !car.Availability;
                        break;
                    case "6":
                        // TODO - Validate 
                        running = false;
                        break;
                    case "7": // Cancel
                        cancel = true;
                        running = false;
                        break;
                }

                Console.CursorVisible = false;
                menuHelper.PrintAddEditCarMenuHeader(newCar);
                (menuParams.left, menuParams.top) = Console.GetCursorPosition();
                menu = menuHelper.GetAddEditCarMenu(car, newCar);

            }

            if (cancel)
                return false;
            else
            {
                if (newCar)
                    dbService.Insert(car);
                else
                    dbService.Update(car);
            }

            return true;
        }
    
        public bool Remove(Car car)
        {
            MenuHelper.PrintAppName();

            Console.Write($"\tDelete car \"{car.Info()}\" and all data for the car? (\"Y/n\"): ");
            if ((Console.ReadLine() ?? "n").ToLower() != "y")
                return false;

            dbService.Delete(car);

            return true;
        }
    }
}
