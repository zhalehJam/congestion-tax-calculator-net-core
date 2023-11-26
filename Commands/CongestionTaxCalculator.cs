using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator.Queries;
using congestion.calculator.Services;

namespace congestion.calculator.Commands
{
    public partial class CongestionTaxCalculator
    {
        private readonly IYearDayType yearDayType;
        private readonly ISpecialTimesTollFee specialTimesTollFee;
        private InfoOfDayToll infoOfDayToll;
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
            infoOfDayToll = new InfoOfDayToll(yearDayType, specialTimesTollFee);
        }

        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            int maxTollAmountPerDay = infoOfDayToll.MaxTollFeeOfEveryDay;
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
            List<(DateTime entranceTime, int fee)> allEnteranceFee = new();
            List<(DateTime entranceTime, int fee)> selectedEnteranceAmount = new();
            var orderedDateTime = dates.OrderBy(dt => dt).ToList();
            for (int i = 0; i < orderedDateTime.Count(); i++)
            {
                (DateTime entranceTime, int fee) newEntranceTollFee = (orderedDateTime[i], infoOfDayToll.GetTollFee(orderedDateTime[i], vehicle));
                allEnteranceFee.Add(newEntranceTollFee);

                selectedEnteranceAmount.Add(CalculateMaxTollFeeInLastHour(allEnteranceFee));
            }
            return selectedEnteranceAmount;
        }

        private (DateTime entranceTime, int fee) CalculateMaxTollFeeInLastHour(List<(DateTime entranceTime, int fee)> allEnteranceFee)
        {
            //TODO: Change to the function with flexiable time getting from DB
            int previuseEnterance = allEnteranceFee.Count-1;
            (DateTime entranceTime, int fee) MaxEnteranceAmount = allEnteranceFee.Last();

            while (previuseEnterance >= 0 && (MaxEnteranceAmount.entranceTime - allEnteranceFee[previuseEnterance].entranceTime).TotalMinutes <= 60)
            {
                if (MaxEnteranceAmount.fee <= allEnteranceFee[previuseEnterance].fee)
                    MaxEnteranceAmount = allEnteranceFee[previuseEnterance];
                previuseEnterance--;
            }
            return MaxEnteranceAmount;
        }
    }
}