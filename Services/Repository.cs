using congestion.calculator.Contracts.Models;
using DBContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace congestion.calculator.Services
{
    public class Repository : IRepository
    {
        private readonly TollCalculationDBContext _dBContext;

        public Repository(TollCalculationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void AddNewSpecialTimeTollFee(SpecialTimeTollFee specialTimeTollFee)
        {
            _dBContext.SpecialTimeTollFees.Add(new DBContext.Models.SpecialTimeTollFee()
            {
                FromTime = specialTimeTollFee.FromTime,
                ToTime = specialTimeTollFee.ToTime,
                TollFee = specialTimeTollFee.TollFee
            });
            _dBContext.SaveChanges();
        }

        public List<DateTime> GetFreeDates()
        {
            var freeDatesOfYear = new List<DateTime>();
            freeDatesOfYear = _dBContext.GetyearDayTypes.Where(d => d.IsfreeDate).Select(d => d.DateOfYear).ToList();
            return freeDatesOfYear;
        }

        public int GetSpecialTimeTollFee(TimeSpan time)
        {
            var specialTime = _dBContext.SpecialTimeTollFees.FirstOrDefault(s => s.FromTime <= time && s.ToTime >= time);
            return specialTime == null ? 0 : specialTime.TollFee;
        }


    }
}
