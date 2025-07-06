
using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    public interface IValidator
    {
        bool EmailValidate(string email, int id, IDatabase<Customer> _db, List<Customer>? items = null);
        bool PhoneNumberValidate(string phone, int id, IDatabase<Customer> _db, List<Customer>? items = null);
        bool CustomerValidate(Customer item);
        bool CarValidate(Car item);
    }
}
