
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    public interface IHelper<T>
    {
        List<T> GetItems();
        List<T> GetItems(int[] id);
        T? GetItemById(int id);
        T? GetItemById(List<T> items, int id);
        T? SelectItem();
        void PrintItems();
        bool AddItem();
        bool EditItem(T item);
        bool AddEditItem(T? item);
        bool RemoveItem(T item);
    }
}
