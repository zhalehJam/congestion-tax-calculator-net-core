using congestion.calculator.Queries;
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
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return _repository.GetFreeDates().Any(e => e.Equals(date));

        }
    }
}