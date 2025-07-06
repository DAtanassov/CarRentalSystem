
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// Common interface for all objects in the system.
    /// </summary>
    public interface IBaseObject
    {
        /// <summary>
        /// Identifier for the object.
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// Flag indicating whether the object is deleted.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
