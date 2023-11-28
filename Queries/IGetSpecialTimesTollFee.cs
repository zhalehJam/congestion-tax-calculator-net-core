using System;

namespace congestion.calculator.Queries
{
    public interface IGetSpecialTimesTollFee
    {
        int GetFee(TimeSpan timeOfDate);
    }
}