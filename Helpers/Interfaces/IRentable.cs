using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    /// <summary>
    /// Defines the rental and return of specific cars, to and from customers.
    /// </summary>
    public interface IRentable
    {
        /// <summary>
        /// Renting a car to a client for a certain period.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="customer"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool RentItem(Car car, Customer customer, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Return of the rental car.
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        bool ReturnItem(Car car);
    }
}
