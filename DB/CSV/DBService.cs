
using CarRentalSystem.DB.Interfaces;

namespace CarRentalSystem.DB.CSV
{
    public class DBService<T> : IDatabase<T>
    {

        IDatabase<T> objectDB;

        public DBService(IDatabase<T> objectDB)
        {
            this.objectDB = objectDB ?? throw new ArgumentNullException(nameof(objectDB), "Database cannot be null");
        }

        public List<T> GetList()
        {
            return objectDB.GetList();
        }
        public T GetById(int id)
        {
            return objectDB.GetById(id);
        }
        public void Insert(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Insert(item);
        }
        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Update(item);
        }
        public void Delete(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Delete(item);
        }

    }
}
