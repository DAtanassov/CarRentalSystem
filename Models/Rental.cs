
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public class Rental : IBaseObject, IRental
    {
        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CarID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CustomerID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime StartDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EndDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Info()
        {
            throw new NotImplementedException();
        }
    }
}
