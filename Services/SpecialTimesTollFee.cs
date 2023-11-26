using System;

namespace congestion.calculator.Services
{
    public class SpecialTimesTollFee : ISpecialTimesTollFee
    {
        public int GetFee(TimeSpan timeOfDate)
        {
            //TODO : should become services and Get from DB
            if (timeOfDate >= new TimeSpan(6, 0, 0) && timeOfDate >= new TimeSpan(6, 29, 0)) return 8;
            else if (timeOfDate >= new TimeSpan(6, 30, 0) && timeOfDate >= new TimeSpan(6, 59, 0)) return 13;
            else if (timeOfDate >= new TimeSpan(7, 0, 0) && timeOfDate >= new TimeSpan(7, 59, 0)) return 18;
            else if (timeOfDate >= new TimeSpan(8, 0, 0) && timeOfDate >= new TimeSpan(8, 29, 0)) return 13;
            else if (timeOfDate >= new TimeSpan(8, 30, 0) && timeOfDate >= new TimeSpan(14, 59, 0)) return 8;
            else if (timeOfDate >= new TimeSpan(15, 0, 0) && timeOfDate >= new TimeSpan(15, 29, 0)) return 13;
            else if (timeOfDate >= new TimeSpan(15, 30, 0) && timeOfDate >= new TimeSpan(16, 59, 0)) return 18;
            else if (timeOfDate >= new TimeSpan(17, 0, 0) && timeOfDate >= new TimeSpan(17, 59, 0)) return 13;
            else if (timeOfDate >= new TimeSpan(18, 0, 0) && timeOfDate >= new TimeSpan(18, 29, 0)) return 8;
            else return 0;
        }
    }
}