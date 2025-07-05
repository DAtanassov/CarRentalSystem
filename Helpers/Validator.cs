
using System.Net.Mail;
using System.Text.RegularExpressions;
using CarRentalSystem.DB.Interfaces;
using CarRentalSystem.Helpers.Interfaces;
using CarRentalSystem.Models.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers
{
    internal class Validator<T> : IValidator<T> where T : IBaseObject, ICustomer
    {
        public bool EmailValidate(string email, int id, IDatabase<T> _db, List<T>? items = null)
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
        public bool PhoneNumberValidate(string phone, int id, IDatabase<T> _db, List<T>? items = null)
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

        public bool CustomerValidate(T item)
        {
            return !string.IsNullOrEmpty(item.Name)
                                && !string.IsNullOrEmpty(item.Email)
                                && !string.IsNullOrEmpty(item.Phone);
        }

    }
}
