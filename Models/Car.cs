
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    /// <summary>
    /// Class representing a car in the car rental system.
    /// </summary>
    public class Car : BaseObject, ICar
    {
        /// <summary>
        /// Property for the manifacturer's make of the car.
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Property for the model of the car.
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Property for the year of manufacture of the car.
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Property for the car's type, such as SUV, Sedan, etc.
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// Property for car availability status.
        /// </summary>
        public bool Availability { get; set; }

        /// <summary>
        /// Constructor for deserializing the Car class.
        /// </summary>
        /// <param name="args"></param>
        public Car(params string[] args)
        {
            if (args != null && args.Any())
            {
                
                if (args.Length > 0)
                    Make = args[0];
                if (args.Length > 1)
                    Model = args[1];
                if (args.Length > 2)
                    Year = int.Parse(args[2]);
                if (args.Length > 3)
                    CarType = args[3];
                if (args.Length > 4)
                    Availability = bool.Parse(args[4]);
                if (args.Length > 5)
                    ID = int.Parse(args[5]);
                if (args.Length > 6)
                    IsDeleted = bool.Parse(args[6]);
            }

        }
        /// <summary>
        /// Constructor for creating a new Car object.
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="carType"></param>
        public Car(string make = "", string model = "", string carType = "")
        {
            Make = make;
            Model = model;
            CarType = carType;
            Year = 0;
            Availability = true;
        }

        /// <summary>
        /// Implementation of the Info method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string Info()
        {
            return $"{Make} {Model}, type: {CarType} ({Year}y.)";
        }
        /// <summary>
        /// Implementation of the ExtendedInfo method from the IInfo interface.
        /// </summary>
        /// <returns></returns>
        public override string ExtendedInfo()
        {
            return Info() + $", ID: {ID}, {(Availability ? "Available" : "Rented")}";
        }
    }
}
