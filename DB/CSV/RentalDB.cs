using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.DB.CSV
{
    public class RentalDB : ReadWriteDB<Rental>, IDatabase<Rental>
    {
        protected readonly static string path = "Rents.csv";

        public void Delete(Rental item)
        {
            List<Rental> list = GetList();

            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            list.RemoveAt(index);

            WriteToFile(list, path);
        }

        public void DeleteByCarId(int id)
        {
            List<Rental> list = GetList();
            
            list.RemoveAll(x => x.CarID == id);
            
            WriteToFile(list, path);
        }

        public void DeleteByCustomerId(int id)
        {
            List<Rental> list = GetList();

            list.RemoveAll(x => x.CustomerID == id);

            WriteToFile(list, path);
        }

        public Rental GetById(int id)
        {
            List<Rental> list = GetList();
            return list.FirstOrDefault(x => x.ID == id) ?? throw new KeyNotFoundException($"Rental with ID {id} not found.");
        }

        public List<Rental> GetList() => base.GetAllItemsFromFile(path);

        public void Insert(Rental item)
        {
            List<Rental> list = GetList();

            item.ID = list.Any() ? list.Max(x => x.ID) + 1 : 1;
            list.Add(item);

            WriteToFile(list, path);
        }

        public void Update(Rental item)
        {
            List<Rental> list = GetList();

            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            list[index] = item;

            WriteToFile(list, path);
        }
        public override void WriteToFile(List<Rental> items, string path) => base.WriteToFile(items, path);
    }
}
