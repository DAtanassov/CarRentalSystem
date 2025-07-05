
namespace CarRentalSystem.Models.Interfaces
{
    public interface ICar
    { 
        string Make { get; set; }
        string Model { get; set; }
        int Year { get; set; }
        string CarType { get; set; }
        bool Availability { get; set; }

        
    }
}
