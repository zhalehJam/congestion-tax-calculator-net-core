using congestion.calculator.Services;
using System;

namespace congestion.calculator.Queries
{
    public class InfoOfDayToll
    {
        public int MaxTollFeeOfEveryDay { get; private set; }

        private readonly IYearDayType yearDayType;
        private readonly ISpecialTimesTollFee specialTimesTollFee;

        public InfoOfDayToll(IYearDayType yearDayType, ISpecialTimesTollFee specialTimesTollFee)
        {
            this.yearDayType = yearDayType;
            this.specialTimesTollFee = specialTimesTollFee;
            GetMaxTollFeeOfEveryDay();

        }
        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date, yearDayType) || IsTollFreeVehicle(vehicle))
                return 0;
            return GetSpecialTimesTollFee(date.TimeOfDay);

        }

        private bool IsTollFreeDate(DateTime date, IYearDayType yearDayType)
        {
            //TODO :Clean the code
            int year = date.Year;

            if (year == 2013)
            {
                return yearDayType.IsOffDay(date);
            }
            return false;
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            string vehicleType = vehicle.GetVehicleType();
            return Enum.IsDefined(typeof(TollFreeVehicles), vehicleType);
        }

        private int GetSpecialTimesTollFee(TimeSpan timeOfDate)
        {
            return specialTimesTollFee.GetFee(timeOfDate);
        }

        private void GetMaxTollFeeOfEveryDay()
        {
            //TODO : get amount from DB
            MaxTollFeeOfEveryDay = 60;
        }

    }
}