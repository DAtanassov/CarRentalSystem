
using CarRentalSystem.Models.Interfaces;
using System.Reflection;

namespace CarRentalSystem.Models
{
    public class Customer : BaseObject, ICustomer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

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
        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email.Trim().ToLower();
            Phone = phone;
        }

        public override string Info()
        {
            return $"{Name}";
        }

        public override string ExtendedInfo()
        {
            return Info() + $", ID: {ID}, e-mail: {Email}, phone: {Phone}";
        }
    }
}
