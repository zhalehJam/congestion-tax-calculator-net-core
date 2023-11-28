using congestion.calculator.Commands;
using congestion.calculator.Contracts.Models;
using congestion.calculator.Queries.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculateToll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollFeeController : ControllerBase
    {
        private readonly IGetyearDayType getyearDayType;
        private readonly IAddNewSpecialTimeTollFeeService addNewSpecialTimeTollFeeService;
        private readonly IGetSpecialTimesTollFee getSpecialTimesTollFee;
        private readonly IEnumerable<IVehicle> vehicle;

        public TollFeeController(IGetyearDayType getyearDayType,
                                 IAddNewSpecialTimeTollFeeService addNewSpecialTimeTollFeeService,
                                 IGetSpecialTimesTollFee getSpecialTimesTollFee,
                                 IEnumerable<IVehicle> vehicle)
        { 
            this.getyearDayType = getyearDayType;
            this.addNewSpecialTimeTollFeeService = addNewSpecialTimeTollFeeService;
            this.getSpecialTimesTollFee = getSpecialTimesTollFee;
            this.vehicle = vehicle;
        }

        [HttpPost]
        public void Post(SpecialTimeTollFee specialTimeTollFee)
        {
            var t = new AddNewSpecialTimeTollFee(specialTimeTollFee, addNewSpecialTimeTollFeeService, getSpecialTimesTollFee);
        }

        [HttpGet]
        public int GetInfo([FromQuery] DateTime[] dates, string vehicleName)
        {
            dates = [DateTime.Parse( "2013-01-14 21:00:00"),
                                    DateTime.Parse("2013-01-14 21:00:00"),
                                    DateTime.Parse("2013-01-15 21:00:00"),
                                    DateTime.Parse("2013-02-07 06:23:27"),
                                    DateTime.Parse("2013-02-07 15:27:00"),
                                    DateTime.Parse("2013-02-08 06:27:00"),
                                    DateTime.Parse("2013-02-08 06:20:27"),
                                    DateTime.Parse("2013-02-08 14:35:00"),
                                    DateTime.Parse("2013-02-08 15:29:00"),
                                    DateTime.Parse("2013-02-08 15:47:00"),
                                    DateTime.Parse("2013-02-08 16:01:00"),
                                    DateTime.Parse("2013-02-08 16:48:00"),
                                    DateTime.Parse("2013-02-08 17:49:00"),
                                    DateTime.Parse("2013-02-08 18:29:00"),
                                    DateTime.Parse("2013-02-08 18:35:00"),
                                    DateTime.Parse("2013-03-26 14:25:00"),
                                    DateTime.Parse("2013-03-28 14:07:27")
            ];

            var specificVehicle = vehicle.FirstOrDefault(x => x.GetVehicleType() == vehicleName);
            var congestionTaxCalculator = new CongestionTaxCalculator(getyearDayType, getSpecialTimesTollFee, true);
            return congestionTaxCalculator.GetTax(specificVehicle, dates);
        }
    }
}
