
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    /// <summary>
    /// Class representing a rental in the car rental system.
    /// </summary>
    public class Rental : BaseObject, IRental
    {
        /// <summary>
        /// Property containing the car identificator
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// Property containing the customer identificator
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Property containing the rental start date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Property containing the rental end date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Constructor for deserializing the Rental class.
        /// </summary>
        /// <param name="args"></param>
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
        /// <summary>
        /// constructor for creating a new Rental object.
        /// </summary>
        /// <param name="carID"></param>
        /// <param name="customerID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public Rental(int carID, int customerID, DateTime startDate, DateTime endDate)
        {
            IsDeleted = false;
            CarID = carID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Implementation of the Info method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string Info()
        {
            Car? car = new CarHelper().GetItemById(ID);
            Customer? customer = new CustomerHelper().GetItemById(CustomerID);
            
            return $"Rent ID: {ID}, Car: {((car == null) ? CarID : car.Info())}, Customer: {((customer == null) ? CustomerID : customer.Info())}";
        }
        /// <summary>
        /// Implementation of the ExtendedInfo method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string ExtendedInfo()
        {
            return Info() + $", Rent from: {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}";
        }
    }
}
