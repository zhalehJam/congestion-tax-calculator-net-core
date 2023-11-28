using System;

namespace congestion.calculator.Queries.Services
{
    public interface IGetSpecialTimesTollFee
    {
        int GetFee(TimeSpan timeOfDate);
    }
}