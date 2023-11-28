using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator.Queries;
using congestion.calculator.Services;

namespace congestion.calculator.Commands
{
    public partial class CongestionTaxCalculator
    {
        private readonly IGetyearDayType GetyearDayType;
        private readonly IGetSpecialTimesTollFee specialTimesTollFee;
        private RoleOfDayToll infoOfDayToll;
        /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param dates   - date and time of all passes on one day
    * @return - the total congestion tax for that day
*/

        public CongestionTaxCalculator(IGetyearDayType GetyearDayType, IGetSpecialTimesTollFee specialTimesTollFee, bool doesCityHaveAnySpecificYear)
        {
            this.GetyearDayType = GetyearDayType;
            this.specialTimesTollFee = specialTimesTollFee;
            infoOfDayToll = new RoleOfDayToll(GetyearDayType, specialTimesTollFee, doesCityHaveAnySpecificYear);
        }

        public int GetTax(IVehicle vehicle, DateTime[] dates)
        {
            int maxTollAmountPerDay = infoOfDayToll.MaxTollFeeOfEveryDay;
            int totalFee = 0;
            List<(DateTime entranceTime, int fee)> selectedEnterance = CalculateTheListOFPayableToll(vehicle, dates);

            //TODO: Problem about midnight hour and entrances

            totalFee = selectedEnterance.GroupBy(e => e.entranceTime.Date)
                                        .Select(w => new { w.Key.Date, dayTaxAmount = w.Sum(e => e.fee) })
                                        .Sum(e => e.dayTaxAmount > maxTollAmountPerDay ? maxTollAmountPerDay : e.dayTaxAmount);
            return totalFee;
        }

        private List<(DateTime entranceTime, int fee)> CalculateTheListOFPayableToll(IVehicle vehicle, DateTime[] dates)
        {
            List<(DateTime entranceTime, int fee)> selectedEnteranceAmount = new();

            var orderedDateTime = dates.OrderBy(dt => dt).ToList();
            for (int i = 0; i < orderedDateTime.Count(); i++)
            {
                (DateTime entranceTime, int fee) newEntranceTollFee = (orderedDateTime[i], infoOfDayToll.GetTollFee(orderedDateTime[i], vehicle));

                selectedEnteranceAmount.Add(newEntranceTollFee);
                selectedEnteranceAmount = ReFillListBasedNewEntrance(selectedEnteranceAmount, newEntranceTollFee.entranceTime);
            }
            return selectedEnteranceAmount;
        }

        private List<(DateTime entranceTime, int fee)> ReFillListBasedNewEntrance(List<(DateTime entranceTime, int fee)> listOFEnteranceAmount,
                                                                                  DateTime newEntranceDate)
        {
            List<(DateTime entranceTime, int fee)> EnteranceAmount = listOFEnteranceAmount;
            var listOfLastHourEntrances = listOFEnteranceAmount.Where(e => (newEntranceDate - e.entranceTime).TotalMinutes < 60)
                                                               .OrderBy(e => e.fee)
                                                               .ToList();

            if (listOfLastHourEntrances.Count() > 1)
            {
                var maxone = listOfLastHourEntrances.Last();
                EnteranceAmount.RemoveAll(r => listOfLastHourEntrances.Any(e => e.Equals(r)));
                EnteranceAmount.Add(maxone);
            }
            return EnteranceAmount;
        }
    }
}