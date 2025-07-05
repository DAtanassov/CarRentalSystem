
using CarRentalSystem.Models.Interfaces;

namespace CarRentalSystem.Models
{
    public class Car : IBaseObject, ICar
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CarType { get; set; }
        public bool Availability { get; set; }

        public Car(params string[] args)
        {
            if (args != null && args.Any())
            {
                if (args.Length > 0)
                    ID = int.Parse(args[0]);
                if (args.Length > 1)
                    Make = args[1];
                if (args.Length > 2)
                    Model = args[2];
                if (args.Length > 3)
                    Year = int.Parse(args[3]);
                if (args.Length > 4)
                    CarType = args[4];
                if (args.Length > 5)
                    Availability = bool.Parse(args[5]);
            }

        }
        public Car(string make = "", string model = "", string carType = "")
        {
            Make = make;
            Model = model;
            CarType = carType;
            Year = 0;
            Availability = true;
        }

        public string Info()
        {
            return $"{Make} {Model}, type {CarType} ({Year}y.)";
        }

        public string ExtendedInfo()
        {
            return Info() + $", ID {ID}, {(Availability ? "Available" : "Rented")}";
        }
    }
}
