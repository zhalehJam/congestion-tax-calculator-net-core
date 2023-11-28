using congestion.calculator.Queries.Services;

namespace congestion.calculator.Services
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return "Car";
        }
    }
}