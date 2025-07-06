
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public abstract class BaseObject : IBaseObject, IInfo
    {
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public abstract string Info();
        public abstract string ExtendedInfo();

    }
}
