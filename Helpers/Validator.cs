using System.Net.Mail;
using System.Text.RegularExpressions;
using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    /// <summary>
    /// Implementation of validation for customer and car entities.
    /// </summary>
    internal class Validator : IValidator
    {
        /// <summary>
        /// Implementation of validation for email address is unique and correctly formatted.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <param name="_db"></param>
        /// <param name="items"></param>
        /// <returns><c>true</c> if the email is valid and unique; otherwise, <c>false</c>.</returns>
        public bool EmailValidate(string email, int id, IDatabase<Customer> _db, List<Customer>? items = null)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }

            if (items == null)
                items = _db.GetList();

            if (items != null && items.Where(i => i.Email == email && i.ID != id).Count() > 0)
                return false;

            return true;
        }
        /// <summary>
        /// Implementation of validation for phone number is unique and correctly formatted.
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <param name="_db"></param>
        /// <param name="items"></param>
        /// <returns><c>true</c> if the phone number is valid and unique; otherwise, <c>false</c>.</returns>
        public bool PhoneNumberValidate(string phone, int id, IDatabase<Customer> _db, List<Customer>? items = null)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            // 123-456-7890, (123) 456-7890, 1234567890
            string pattern = @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
            Regex validateRegex = new Regex(pattern);
            if (!validateRegex.IsMatch(phone))
                return false;

            if (items == null)
                items = _db.GetList();

            if (items != null && items.Where(i => i.Phone == phone && i.ID != id).Count() > 0)
                return false;

            return true;
        }
        /// <summary>
        /// Implementation of validation for a <see cref="Customer"/> object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><c>true</c> if the customer is valid; otherwise, <c>false</c>.</returns>
        public bool CustomerValidate(Customer item)
        {
            return !string.IsNullOrEmpty(item.Name)
                                && !string.IsNullOrEmpty(item.Email)
                                && !string.IsNullOrEmpty(item.Phone);
        }
        /// <summary>
        /// Implementation of validation for a <see cref="Car"/> object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns><c>true</c> if the car is valid; otherwise, <c>false</c>.</returns>
        public bool CarValidate(Car item)
        {
            return !string.IsNullOrEmpty(item.Make)
                            && !string.IsNullOrEmpty(item.Model)
                            && !string.IsNullOrEmpty(item.CarType)
                            && item.Year > 0;
        }
        
    }
}
