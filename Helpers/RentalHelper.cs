using CarRentalSystem.DB.CSV;
using CarRentalSystem.Models;
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Helpers
{
    public class RentalHelper
    {
        // Object for read and write to database file
        private static readonly DBService<Rental> dbService = new DBService<Rental>(new RentalDB());

        public List<Rental> GetItems() => dbService.GetList();
        public Rental? GetItemByCarId(int id)
        {
            List<Rental> items = GetItems();

            if (items.Count == 0)
                return null;

            return items.FirstOrDefault(i => !i.IsDeleted && i.CarID == id);
        }
        public bool AddItem(Car car, Customer customer, DateTime startDate, DateTime endDate)
        {
            Rental rental = new Rental(car.ID, customer.ID, startDate, endDate);

            dbService.Insert(rental);

            CarHelper carHelper = new CarHelper();
            if (!carHelper.ChangeAlailability(car, false))
            {
                dbService.Delete(rental);
                return false;
            }

            return true;
        }

        public bool ReturnItem(Car car)
        {
            Rental? rental = GetItemByCarId(car.ID);

            if (rental == null)
                return false;

            rental.IsDeleted = true;
            dbService.Update(rental);
            CarHelper carHelper = new CarHelper();
            if (!carHelper.ChangeAlailability(car, true))
            {
                rental.IsDeleted = false;
                dbService.Update(rental);
                return false;
            }

            return true;
        }
    }
}
