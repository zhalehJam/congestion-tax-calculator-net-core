using System;

namespace congestion.calculator.Queries
{
    public interface IYearDayType
    {
        bool IsOffDay(DateTime date);
    }
}