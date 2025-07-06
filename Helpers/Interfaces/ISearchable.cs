
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    /// <summary>
    /// Searchable interface for searching cars in a list.
    /// </summary>
    public interface ISearchable
    {
        /// <summary>
        /// Searches for a car in a list of cars based on user input.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Car? SearchCars(List<Car>? items);
    }
}
