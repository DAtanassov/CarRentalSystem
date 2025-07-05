
namespace CarRentalSystem.Models.Interfaces
{
    public interface IBaseObject
    {
        int ID { get; set; }
        string Info();
        string ExtendedInfo();
    }
}
