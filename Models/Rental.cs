
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Interfaces;
using System.Numerics;
using System.Xml.Linq;

namespace CarRentalSystem.Models
{
    public class Rental : IBaseObject, IRental
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Rental(params string[] args)
        {
            if (args != null && args.Any())
            {
                if (args.Length > 0)
                    ID = int.Parse(args[0]);
                if (args.Length > 1)
                    IsDeleted = bool.Parse(args[1]);
                if (args.Length > 2)
                    CarID = int.Parse(args[2]);
                if (args.Length > 3)
                    CustomerID = int.Parse(args[3]);
                if (args.Length > 4)
                    StartDate = DateTime.Parse(args[4]);
                if (args.Length > 5)
                    EndDate = DateTime.Parse(args[5]);
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

        public string Info()
        {
            Car? car = new CarHelper().GetItemById(ID);
            Customer? customer = new CustomerHelper().GetItemById(CustomerID);
            
            return $"Rent ID: {ID}, Car: {((car == null) ? CarID : car.Info())}, Customer: {((customer == null) ? CustomerID : customer.Info())}";
        }

        public string ExtendedInfo()
        {
            return Info() + $", Rent from: {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}";
        }
    }
}
