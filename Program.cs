using CarRentalSystem.Models;

namespace CarRentalSystem
{
    /// <summary>
    /// Program class serves as the entry point for the Car Rental System application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create an instance of the Menu class and run it
            new Menu().Run();
        }
    }
}
