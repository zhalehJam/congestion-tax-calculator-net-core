using congestion.calculator.Contracts;
using System;

namespace congestion.calculator.Queries
{
    public class RoleOfDayToll
    {
        public int MaxTollFeeOfEveryDay { get; private set; }
        public int SpecificYear { get; private set; }
        public bool DoesCityHaveAnySpecificYear { get; set; }
        private readonly IGetyearDayType GetyearDayType;
        private readonly IGetSpecialTimesTollFee specialTimesTollFee;

        public RoleOfDayToll(IGetyearDayType GetyearDayType, IGetSpecialTimesTollFee specialTimesTollFee, bool doesCityHaveAnySpecificYear)
        {
            this.GetyearDayType = GetyearDayType;
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

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date, GetyearDayType) || IsTollFreeVehicle(vehicle))
                return 0;
            return GetSpecialTimesTollFee(date.TimeOfDay);

        }

        private bool IsTollFreeDate(DateTime date, IGetyearDayType GetyearDayType)
        {
            if (DoesCityHaveAnySpecificYear && date.Year == SpecificYear)
            {
                return GetyearDayType.IsOffDay(date);
            }
            return false;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
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