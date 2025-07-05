
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Interfaces;
using System.Numerics;
using System.Xml.Linq;

namespace CarRentalSystem.Models
{
    public class Rental : BaseObject, IRental
    {
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Rental(params string[] args)
        {
            if (args != null && args.Any())
            {
                if (args.Length > 0)
                    CarID = int.Parse(args[0]);
                if (args.Length > 1)
                    CustomerID = int.Parse(args[1]);
                if (args.Length > 2)
                    StartDate = DateTime.Parse(args[2]);
                if (args.Length > 3)
                    EndDate = DateTime.Parse(args[3]);
                if (args.Length > 4)
                    ID = int.Parse(args[4]);
                if (args.Length > 5)
                    IsDeleted = bool.Parse(args[5]);
            }
        }

        public Rental(int carID, int customerID, DateTime startDate, DateTime endDate)
        {
            IsDeleted = false;
            CarID = carID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string Info()
        {
            Car? car = new CarHelper().GetItemById(ID);
            Customer? customer = new CustomerHelper().GetItemById(CustomerID);
            
            return $"Rent ID: {ID}, Car: {((car == null) ? CarID : car.Info())}, Customer: {((customer == null) ? CustomerID : customer.Info())}";
        }

        public override string ExtendedInfo()
        {
            return Info() + $", Rent from: {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}";
        }
    }
}
