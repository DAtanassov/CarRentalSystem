
namespace CarRentalSystem.Models.Interfaces
{
    /// <summary>
    /// A common interface for defining a start and exit menu interface.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Starts the execution menu.
        /// </summary>
        void Run();
        /// <summary>
        /// Exits the application.
        /// </summary>
        void ExitMenu();
    }
}
