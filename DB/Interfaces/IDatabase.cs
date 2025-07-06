
namespace CarRentalSystem.DB.Interfaces
{
    /// <summary>
    /// Represents a generic interface for basic CRUD operations on a data store.
    /// </summary>
    /// <typeparam name="T">The type of object to be stored in the database.</typeparam>
    public interface IDatabase<T>
    {
        /// <summary>
        /// Retrieves all items of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <returns>A list of all <typeparamref name="T"/> items.</returns>
        List<T> GetList();
        
        /// <summary>
        /// Retrieves a single item by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the item to retrieve.</param>
        /// <returns>The item with the specified <paramref name="id"/> if found; otherwise, default value of <typeparamref name="T"/>.</returns>
        T GetById(int id);

        /// <summary>
        /// Inserts a new item into the database.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        void Insert(T item);

        /// <summary>
        /// Updates an existing item in the database.
        /// </summary>
        /// <param name="item">The item with updated values.</param>
        void Update(T item);

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(T item);
    }
}
