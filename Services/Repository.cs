using DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace congestion.calculator.Services
{
    public class Repository : IRepository
    {
        private readonly TollCalculationDBContext _dBContext;

        public Repository(TollCalculationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public List<DateTime> GetFreeDates()
        {
            var freeDatesOfYear = new List<DateTime>();
            freeDatesOfYear = _dBContext.YearDayTypes.Where(d => d.IsfreeDate).Select(d => d.DateOfYear).ToList();
            return freeDatesOfYear;
        }

        public int GetSpecialTimeTollFee(TimeSpan time)
        {
            var specialTime = _dBContext.SpecialTimeTollFees.FirstOrDefault(s => s.FromTime <= time && s.ToTime >= time);
            return specialTime == null ? 0 : specialTime.TollFee;
        }

    }
}
