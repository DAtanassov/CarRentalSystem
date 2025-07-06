
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    /// <summary>
    /// An abstract base class representing a common object in the system.
    /// </summary>
    public abstract class BaseObject : IBaseObject, IInfo
    {
        /// <summary>
        /// Identifier for the object.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Flag indicating whether the object is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Shot description of the object.
        /// </summary>
        /// <returns></returns>
        public abstract string Info();
        /// <summary>
        /// Detailed information about the object.
        /// </summary>
        /// <returns></returns>
        public abstract string ExtendedInfo();

    }
}
