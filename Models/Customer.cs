
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public class Customer : IBaseObject, ICustomer
    {
        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Info()
        {
            throw new NotImplementedException();
        }
    }
}
