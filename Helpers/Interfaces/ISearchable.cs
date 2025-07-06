
using CarRentalSystem.Models;

namespace CarRentalSystem.Helpers.Interfaces
{
    public interface ISearchable
    {
        Car? SearchCars(List<Car>? items);
    }
}
