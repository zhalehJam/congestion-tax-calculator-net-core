using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congestion.calculator.Contracts.Models
{
    public class SpecialTimeTollFee
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int TollFee { get; set; }
    }
}
