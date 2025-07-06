using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    public interface IRentable
    {
        bool RentItem(Car car, Customer customer, DateTime startDate, DateTime endDate);
        bool ReturnItem(Car car);
    }
}
