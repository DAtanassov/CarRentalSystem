
namespace CarRentalSystem.Helpers.Interfaces
{
    /// <summary>
    /// Defines a common helper interface for performing common CRUD operations on elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHelper<T>
    {
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A list containing all items of type <typeparamref name="T"/>.</returns>
        List<T> GetItems();

        /// <summary>
        /// Retrieves items by array of IDs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list containing all items of type <typeparamref name="T"/>.</returns>
        List<T> GetItems(int[] id);

        /// <summary>
        /// Retrieves a single item by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single item of type <typeparamref name="T"/> or null if not found.</returns>
        T? GetItemById(int id);

        /// <summary>
        /// Retrieves an item with the specified ID from a given list.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="id"></param>
        /// <returns>A single item of type <typeparamref name="T"/> or null if not found.</returns>
        T? GetItemById(List<T> items, int id);

        /// <summary>
        /// Select an item from a data source
        /// </summary>
        /// <returns>A single item of type <typeparamref name="T"/> or null if not found.</returns>
        T? SelectItem();

        /// <summary>
        /// Displays all items in a user-friendly format.
        /// </summary>
        void PrintItems();

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <returns></returns>
        bool AddItem();

        /// <summary>
        /// Edit an existing item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool EditItem(T item);

        /// <summary>
        /// Implementation of adding or editing an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
        bool AddEditItem(T? item);

        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
        bool RemoveItem(T item);
    }
}
