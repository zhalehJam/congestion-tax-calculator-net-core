using System;
using System.Collections.Generic;

namespace congestion.calculator.Services
{
    public interface IRepository
    {
        List<DateTime> GetFreeDates();
        int GetSpecialTimeTollFee(TimeSpan time);
    }
}