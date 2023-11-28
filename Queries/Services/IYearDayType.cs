using System;

namespace congestion.calculator.Queries.Services
{
    public interface IGetyearDayType
    {
        bool IsOffDay(DateTime date);
    }
}