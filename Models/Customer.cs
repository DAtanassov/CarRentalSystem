
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    /// <summary>
    /// Class representing a customer in the car rental system.
    /// </summary>
    public class Customer : BaseObject, ICustomer
    {
        /// <summary>
        /// Property for the customer's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Property for the customer's e-mail address.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Property for the customer's phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Constructor for deserializing the Customer class.
        /// </summary>
        /// <param name="args"></param>
        public Customer(params string[] args)
        {
            if (args != null && args.Any())
            {
                if (args.Length > 0)
                    Name = args[0];
                if (args.Length > 1)
                    Email = args[1];
                if (args.Length > 2)
                    Phone = args[2];
                if (args.Length > 3)
                    ID = int.Parse(args[3]);
                if (args.Length > 4)
                    IsDeleted = bool.Parse(args[4]);
            }

        }
        /// <summary>
        /// constructor for creating a new Customer object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email.Trim().ToLower();
            Phone = phone;
        }

        /// <summary>
        /// Implementation of the Info method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string Info()
        {
            return $"{Name}";
        }
        /// <summary>
        /// Implementation of the ExtendedInfo method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string ExtendedInfo()
        {
            return Info() + $", ID: {ID}, e-mail: {Email}, phone: {Phone}";
        }
    }
}
