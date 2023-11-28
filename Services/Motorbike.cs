using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using congestion.calculator.Queries.Services;

namespace congestion.calculator.Services
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}