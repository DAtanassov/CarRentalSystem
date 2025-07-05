
namespace CarRentalSystem.Models.Interfaces
{
    public interface IRental
    {
        int CarID { get; set; }
        int CustomerID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        string Info();
    }
}
