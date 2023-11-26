using System;
using System.Collections.Generic;

namespace congestion.calculator
{
    public interface IRepository
    {
        List<DateTime> GetFreeDates();
        int GetSpecialTimesTollFee();
    }
}