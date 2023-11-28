using System;

namespace congestion.calculator.Queries
{
    public interface ISpecialTimesTollFee
    {
        int GetFee(TimeSpan timeOfDate);
    }
}