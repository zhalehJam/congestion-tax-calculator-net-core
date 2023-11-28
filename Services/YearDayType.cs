using congestion.calculator.Queries.Services;
using System;
using System.Linq;

namespace congestion.calculator.Services
{
    public class GetyearDayType : IGetyearDayType
    {
        private readonly IRepository _repository;

        public GetyearDayType(IRepository repository)
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