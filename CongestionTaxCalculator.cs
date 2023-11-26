using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
public partial class CongestionTaxCalculator
{
    private readonly IYearDayType yearDayType;
    private readonly ISpecialTimesTollFee specialTimesTollFee;

    /**
* Calculate the total toll fee for one day
*
* @param vehicle - the vehicle
* @param dates   - date and time of all passes on one day
* @return - the total congestion tax for that day
*/

    public CongestionTaxCalculator(IYearDayType yearDayType, ISpecialTimesTollFee specialTimesTollFee)
    {
        this.yearDayType = yearDayType;
        this.specialTimesTollFee = specialTimesTollFee;
    }
    public int GetTax(Vehicle vehicle, DateTime[] dates)
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
        return GetSpecialTimesTollFee(date.TimeOfDay);

    }

    private int GetSpecialTimesTollFee(TimeSpan timeOfDate)
    {
        return specialTimesTollFee.GetFee(timeOfDate);
    }

    private Boolean IsTollFreeDate(DateTime date, IYearDayType yearDayType)
    {
        //TODO :Clean the code
        int year = date.Year;

        if (year == 2013)
        {
            return yearDayType.IsOffDay(date);
        }
        return false;
    }
}