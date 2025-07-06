
using CarRentalSystem.DB.Interfaces;

namespace CarRentalSystem.DB.CSV
{
    /// <summary>
    /// Provides a service layer that delegates database operations to a specified <see cref="IDatabase{T}"/> implementation.
    /// Includes null-checking logic for safe interaction with the underlying database.
    /// </summary>
    /// <typeparam name="T">The type of the entity managed by this service.</typeparam>
    public class DBService<T> : IDatabase<T>
    {
        /// <summary>
        /// The internal database object to which operations are delegated.
        /// </summary>
        IDatabase<T> objectDB;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBService{T}"/> class with the specified database implementation.
        /// </summary>
        /// <param name="objectDB">The <see cref="IDatabase{T}"/> implementation to use for data operations.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="objectDB"/> is <c>null</c>.</exception>
        public DBService(IDatabase<T> objectDB)
        {
            this.objectDB = objectDB ?? throw new ArgumentNullException(nameof(objectDB), "Database cannot be null");
        }

        /// <summary>
        /// Retrieves all items of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <returns>A list of all <typeparamref name="T"/> items.</returns>
        public List<T> GetList()
        {
            return objectDB.GetList();
        }

        /// <summary>
        /// Retrieves a single item by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to retrieve.</param>
        /// <returns>The item with the specified <paramref name="id"/> if found; otherwise, default value of <typeparamref name="T"/>.</returns>
        public T GetById(int id)
        {
            return objectDB.GetById(id);
        }

        /// <summary>
        /// Inserts a new item into the database.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        public void Insert(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Insert(item);
        }

        /// <summary>
        /// Updates an existing item in the database.
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Update(item);
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public void Delete(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Item cannot be null");
            objectDB.Delete(item);
        }

    }
}
