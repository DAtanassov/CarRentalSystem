
using CarRentalSystem.Models.Interfaces;
using System.Reflection;

namespace CarRentalSystem.Models
{
    public class Customer : IBaseObject, ICustomer
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Customer(params string[] args)
        {
            if (args != null && args.Any())
            {
                if (args.Length > 0)
                    ID = int.Parse(args[0]);
                if (args.Length > 1)
                    IsDeleted = bool.Parse(args[1]);
                if (args.Length > 2)
                    Name = args[2];
                if (args.Length > 3)
                    Email = args[3];
                if (args.Length > 4)
                    Phone = args[4];
            }

        }
        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email.Trim().ToLower();
            Phone = phone;
        }

        public string Info()
        {
            return $"{Name}";
        }

        public string ExtendedInfo()
        {
            return Info() + $", ID: {ID}, e-mail: {Email}, phone: {Phone}";
        }
    }
}
