using congestion.calculator.Contracts.Models;
using System;
using System.Collections.Generic;

namespace congestion.calculator.Services
{
    public interface IRepository
    {
        void AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee);
        List<DateTime> GetFreeDates();
        int GetSpecialTimeTollFee(TimeSpan time);
    }
}