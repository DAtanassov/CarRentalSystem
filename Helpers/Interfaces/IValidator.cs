
using CarRentalSystem.DB.Interfaces;

namespace CarRentalSystem.Helpers.Interfaces
{
    public interface IValidator<T>
    {
        bool EmailValidate(string email, int id, IDatabase<T> _db, List<T>? items = null);
        bool PhoneNumberValidate(string phone, int id, IDatabase<T> _db, List<T>? items = null);
        bool CustomerValidate(T item);
    }
}
