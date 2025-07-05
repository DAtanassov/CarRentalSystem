
using CarRentalSystem.Helpers;
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public class Rental : IBaseObject, IRental
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Info()
        {
            CarHelper carHelper = new CarHelper();
            Car car = carHelper.GetItemById(ID);
            CustomerHelper customerHelper = new CustomerHelper();
            Customer customer = customerHelper.GetItemById(CustomerID);
            
            return $"Rent ID: {ID}, Car: {((car == null) ? CarID : car.Info())}, Customer: {((customer == null) ? CustomerID : customer.Info())}";
        }

        public string ExtendedInfo()
        {
            return Info() + $", Rent from: {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}";
        }
    }
}
