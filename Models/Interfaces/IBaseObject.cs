
namespace CarRentalSystem.Models.Interfaces
{
    public interface IBaseObject
    {
        int ID { get; set; }
        bool IsDeleted { get; set; }
    }
}
