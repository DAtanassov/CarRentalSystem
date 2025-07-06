
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// Common interface for a <see cref="Rental"/> object/>.
    /// </summary>
    public interface IRental
    {
        /// <summary>
        /// Property containing the car identificator
        /// </summary>
        int CarID { get; set; }
        /// <summary>
        /// Property containing the customer identificator
        /// </summary>
        int CustomerID { get; set; }
        /// <summary>
        /// Property containing the rental start date
        /// </summary>
        DateTime StartDate { get; set; }
        /// <summary>
        /// Property containing the rental end date
        /// </summary>
        DateTime EndDate { get; set; }

    }
}
