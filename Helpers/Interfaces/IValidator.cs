
using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    /// <summary>
    /// Defines a set of validation methods for customer and car entities.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates whether the given email address is unique and correctly formatted.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <param name="_db"></param>
        /// <param name="items"></param>
        /// <returns><c>true</c> if the email is valid and unique; otherwise, <c>false</c>.</returns>
        bool EmailValidate(string email, int id, IDatabase<Customer> _db, List<Customer>? items = null);

        /// <summary>
        /// Validates whether the given phone number is unique and correctly formatted.
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <param name="_db"></param>
        /// <param name="items"></param>
        /// <returns><c>true</c> if the phone number is valid and unique; otherwise, <c>false</c>.</returns>
        bool PhoneNumberValidate(string phone, int id, IDatabase<Customer> _db, List<Customer>? items = null);

        /// <summary>
        /// Validates the integrity and business rules of a <see cref="Customer"/> object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><c>true</c> if the customer is valid; otherwise, <c>false</c>.</returns>
        bool CustomerValidate(Customer item);

        /// <summary>
        /// Validates the integrity and business rules of a <see cref="Car"/> object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><c>true</c> if the car is valid; otherwise, <c>false</c>.</returns>
        bool CarValidate(Car item);
    }
}
