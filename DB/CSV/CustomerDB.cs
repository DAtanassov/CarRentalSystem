using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.DB.CSV
{
    /// <summary>
    /// Provides a CRUD operations on a data store for the Customer class in a .csv file.
    /// </summary>
    internal class CustomerDB : ReadWriteDB<Customer>, IDatabase<Customer>
    {
        protected readonly static string path = "Customers.csv";

        public void Delete(Customer item)
        {
            List<Customer> list = GetList();

            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            new RentalDB().DeleteByCustomerId(item.ID);

            list.RemoveAt(index);

            WriteToFile(list, path);
        }

        public Customer GetById(int id)
        {
            List<Customer> list = GetList();
            return list.FirstOrDefault(x => x.ID == id) ?? throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        public List<Customer> GetList() => base.GetAllItemsFromFile(path);

        public void Insert(Customer item)
        {
            List<Customer> list = GetList();

            item.ID = list.Any() ? list.Max(x => x.ID) + 1 : 1;
            list.Add(item);

            WriteToFile(list, path);
        }

        public void Update(Customer item)
        {
            List<Customer> list = GetList();

            int index = list.FindIndex(x => x.ID == item.ID);
            if (index == -1)
                return;

            list[index] = item;

            WriteToFile(list, path);
        }
        public override void WriteToFile(List<Customer> items, string path) => base.WriteToFile(items, path);

    }
}
