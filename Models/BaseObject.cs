
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public abstract class BaseObject : IBaseObject
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }

        public abstract string ExtendedInfo();

        public abstract string Info();
    }
}
