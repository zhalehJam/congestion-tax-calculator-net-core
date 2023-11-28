using congestion.calculator.Contracts;
using System;

namespace congestion.calculator.Queries
{
    public class RoleOfDayToll
    {
        public int MaxTollFeeOfEveryDay { get; private set; }
        public int SpecificYear { get; private set; }
        public bool DoesCityHaveAnySpecificYear { get; set; }
        private readonly IYearDayType yearDayType;
        private readonly ISpecialTimesTollFee specialTimesTollFee;

        public RoleOfDayToll(IYearDayType yearDayType, ISpecialTimesTollFee specialTimesTollFee, bool doesCityHaveAnySpecificYear)
        {
            this.yearDayType = yearDayType;
            this.specialTimesTollFee = specialTimesTollFee;
            DoesCityHaveAnySpecificYear = doesCityHaveAnySpecificYear;
            GetMaxTollFeeOfEveryDay();
            SetSpecificYear(doesCityHaveAnySpecificYear);
        }

        private void SetSpecificYear(bool DoesCityHaveAnySpecificYear)
        {
            if (DoesCityHaveAnySpecificYear)
            {
                SpecificYear = 2013;
            }
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date, yearDayType) || IsTollFreeVehicle(vehicle))
                return 0;
            return GetSpecialTimesTollFee(date.TimeOfDay);

        }

        private bool IsTollFreeDate(DateTime date, IYearDayType yearDayType)
        {
            if (DoesCityHaveAnySpecificYear && date.Year == SpecificYear)
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