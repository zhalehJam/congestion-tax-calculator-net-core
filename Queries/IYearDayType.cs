using System;

namespace congestion.calculator.Queries
{
    public interface IGetyearDayType
    {
        bool IsOffDay(DateTime date);
    }
}