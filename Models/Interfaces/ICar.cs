
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// Common interface for a <see cref="Car"/> object/>.
    /// </summary>
    public interface ICar
    {
        /// <summary>
        /// Property for the manifacturer's make of the car.
        /// </summary>
        string Make { get; set; }
        /// <summary>
        /// Property for the model of the car.
        /// </summary>
        string Model { get; set; }
        /// <summary>
        /// Property for the year of manufacture of the car.
        /// </summary>
        int Year { get; set; }
        /// <summary>
        /// Property for the car's type, such as SUV, Sedan, etc.
        /// </summary>
        string CarType { get; set; }
        /// <summary>
        /// Property for car availability status.
        /// </summary>
        bool Availability { get; set; }

    }
}
