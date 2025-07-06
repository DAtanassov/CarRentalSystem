
using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.DB.CSV
{
    /// <summary>
    /// Provides a CRUD operations on a data store for the Car class in a .csv file.
    /// </summary>
    public class CarDB : ReadWriteDB<Car>, IDatabase<Car>
    {
        protected readonly static string path = "Cars.csv";

        public void Delete(Car item)
        {
            List<Car> list = GetList();

            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            new RentalDB().DeleteByCarId(item.ID);

            list.RemoveAt(index);

            WriteToFile(list, path);
        }

        public Car GetById(int id)
        {
            List<Car> list = GetList();
            return list.FirstOrDefault(x => x.ID == id) ?? throw new KeyNotFoundException($"Car with ID {id} not found.");
        }

        public List<Car> GetList() => base.GetAllItemsFromFile(path);

        public void Insert(Car item)
        {
            List<Car> list = GetList();

            item.ID = list.Any() ? list.Max(x => x.ID) + 1 : 1;
            list.Add(item);

            WriteToFile(list, path);
        }

        public void Update(Car item)
        {
            List<Car> list = GetList();
            
            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            list[index] = item;

            WriteToFile(list, path);
        }
        public override void WriteToFile(List<Car> items, string path) => base.WriteToFile(items, path);

    }
}
