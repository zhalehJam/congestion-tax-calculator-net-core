using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
public partial class CongestionTaxCalculator
{
    private readonly IYearDayType yearDayType;
    
    /**
* Calculate the total toll fee for one day
*
* @param vehicle - the vehicle
* @param dates   - date and time of all passes on one day
* @return - the total congestion tax for that day
*/

    public int GetTax(Vehicle vehicle, DateTime[] dates )
    {
        //TODO: Get from DB
        int maxTollAmountPerDay = 60;
        int totalFee = 0;
        List<(DateTime entranceTime, int fee)> selectedEnteranceAmount = CalculateTheListOFPayableToll(vehicle, dates);
        //TODO: Problem about midnight hour and entrances
        totalFee = selectedEnteranceAmount.GroupBy(e => e.entranceTime.Date)
                                          .Select(w => new { w.Key.Date, dayTaxAmount = w.Sum(e => e.fee) })
                                          .Sum(e => e.dayTaxAmount > maxTollAmountPerDay ? maxTollAmountPerDay : e.dayTaxAmount);
        return totalFee;
    }

    private List<(DateTime entranceTime, int fee)> CalculateTheListOFPayableToll(Vehicle vehicle, DateTime[] dates)
    {
        List<(DateTime entranceTime, int fee)> allEnteranceFee = new List<(DateTime entranceTime, int fee)>();
        List<(DateTime entranceTime, int fee)> selectedEnteranceAmount = new List<(DateTime entranceTime, int fee)>();
        var orderedDateTime = dates.OrderBy(dt => dt).ToList();
        for (int i = 0; i < orderedDateTime.Count(); i++)
        {
            (DateTime entranceTime, int fee) newEntranceTollFee = (orderedDateTime[i], GetTollFee(orderedDateTime[i], vehicle));
            allEnteranceFee.Add(newEntranceTollFee);

            selectedEnteranceAmount.Add(GetMaxTollFeeInLastHour(allEnteranceFee));
        }
        return selectedEnteranceAmount;
    }

    private (DateTime entranceTime, int fee) GetMaxTollFeeInLastHour(List<(DateTime entranceTime, int fee)> allEnteranceFee)
    {
        //TODO: Change to the function with flexiable time getting from DB
        int previuseEnterance = allEnteranceFee.Count;
        (DateTime entranceTime, int fee) MaxEnteranceAmount = allEnteranceFee.Last();

        while ((MaxEnteranceAmount.entranceTime - allEnteranceFee[previuseEnterance].entranceTime).TotalMinutes <= 60 && previuseEnterance >= 0)
        {
            if (MaxEnteranceAmount.fee <= allEnteranceFee[previuseEnterance].fee)
                MaxEnteranceAmount = allEnteranceFee[previuseEnterance];
            previuseEnterance--;
        }
        return MaxEnteranceAmount;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        String vehicleType = vehicle.GetVehicleType();
        return Enum.IsDefined(typeof(TollFreeVehicles), vehicleType);
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date, yearDayType) || IsTollFreeVehicle(vehicle))
            return 0;

        TimeSpan timeOfDate = date.TimeOfDay;

        return GetSpecialTimesTollFee(timeOfDate);

    }

    private int GetSpecialTimesTollFee(TimeSpan timeOfDate)
    {
        //TODO : should become services and Get from DB
        if (timeOfDate >= new TimeSpan(6, 0, 0) && timeOfDate >= new TimeSpan(6, 29, 0)) return 8;
        else if (timeOfDate >= new TimeSpan(6, 30, 0) && timeOfDate >= new TimeSpan(6, 59, 0)) return 13;
        else if (timeOfDate >= new TimeSpan(7, 0, 0) && timeOfDate >= new TimeSpan(7, 59, 0)) return 18;
        else if (timeOfDate >= new TimeSpan(8, 0, 0) && timeOfDate >= new TimeSpan(8, 29, 0)) return 13;
        else if (timeOfDate >= new TimeSpan(8, 30, 0) && timeOfDate >= new TimeSpan(14, 59, 0)) return 8;
        else if (timeOfDate >= new TimeSpan(15, 0, 0) && timeOfDate >= new TimeSpan(15, 29, 0)) return 13;
        else if (timeOfDate >= new TimeSpan(15, 30, 0) && timeOfDate >= new TimeSpan(16, 59, 0)) return 18;
        else if (timeOfDate >= new TimeSpan(17, 0, 0) && timeOfDate >= new TimeSpan(17, 59, 0)) return 13;
        else if (timeOfDate >= new TimeSpan(18, 0, 0) && timeOfDate >= new TimeSpan(18, 29, 0)) return 8;
        else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date, IYearDayType yearDayType)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            return yearDayType.IsOffDay(date);
            //TODO : Should define the services and Get from DB

            //if (month == 1 && day == 1 ||
            //    month == 3 && (day == 28 || day == 29) ||
            //    month == 4 && (day == 1 || day == 30) ||
            //    month == 5 && (day == 1 || day == 8 || day == 9) ||
            //    month == 6 && (day == 5 || day == 6 || day == 21) ||
            //    month == 7 ||
            //    month == 11 && day == 1 ||
            //    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            //{
            //    return true;
            //}
        }
        return false;
    }
}