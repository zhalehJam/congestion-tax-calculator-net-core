using System;
using System.Linq;

namespace congestion.calculator.Services
{
    public class YearDayType : IYearDayType
    {
        private readonly IRepository _repository;

        public YearDayType(IRepository repository)
        {
            _repository = repository;
        }
        public bool IsOffDay(DateTime date)
        {

            //TODO : Should define the services and Get from DB

            //return _repository.GetFreeDates().Any(e => e.Equals(date));

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
            return false;
        }
    }
}