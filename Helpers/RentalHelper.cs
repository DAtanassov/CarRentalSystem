using CarRentalSystem.DB.CSV;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    /// <summary>
    /// Helper class for managing rentals of cars.
    /// </summary>
    public class RentalHelper : IRentable
    {
        /// <summary>
        /// Object for read and write to database file
        /// </summary>
        private static readonly DBService<Rental> dbService = new DBService<Rental>(new RentalDB());
        /// <summary>
        /// Retrieves a list of all rentals.
        /// </summary>
        /// <returns>A list of all items.</returns>
        public List<Rental> GetItems() => dbService.GetList();
        /// <summary>
        /// Retrieves a item of rentals by Car ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single item or null if not found.</returns>
        public Rental? GetItemByCarId(int id)
        {
            List<Rental> items = GetItems();

            if (items.Count == 0)
                return null;

            return items.FirstOrDefault(i => !i.IsDeleted && i.CarID == id);
        }
        /// <summary>
        /// Implements the logic for renting a car to a customer.
        /// </summary>
        /// <param name="car"></param>
        /// <param name="customer"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool RentItem(Car car, Customer customer, DateTime startDate, DateTime endDate)
        {
            Rental rental = new Rental(car.ID, customer.ID, startDate, endDate);

            dbService.Insert(rental);

            CarHelper carHelper = new CarHelper();
            if (!carHelper.ChangeAvailability(car, false))
            {
                dbService.Delete(rental);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Implements the logic for returning a rented car.
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool ReturnItem(Car car)
        {
            Rental? rental = GetItemByCarId(car.ID);

            if (rental == null)
                return false;

            rental.IsDeleted = true;
            dbService.Update(rental);
            CarHelper carHelper = new CarHelper();
            if (!carHelper.ChangeAvailability(car, true))
            {
                rental.IsDeleted = false;
                dbService.Update(rental);
                return false;
            }

            return true;
        }
    }
}
