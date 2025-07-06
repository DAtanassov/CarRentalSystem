
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// Common interface for objects that provide information.
    /// </summary>
    public interface IInfo
    {
        /// <summary>
        /// Shot description of the object.
        /// </summary>
        /// <returns></returns>
        string Info();
        /// <summary>
        /// Detailed information about the object.
        /// </summary>
        /// <returns></returns>
        string ExtendedInfo();
    }
}
