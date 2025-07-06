
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// Common interface for a <see cref="Customer"/> object/>.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Property for the customer's name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Property for the customer's e-mail address.
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// Property for the customer's phone number.
        /// </summary>
        string Phone { get; set; }

    }
}
