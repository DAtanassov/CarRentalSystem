
namespace CarRentalSystem.Models.Interfaces
{
    public interface ICustomer
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }

        string Info();
    }
}
