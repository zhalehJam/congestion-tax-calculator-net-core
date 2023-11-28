using Microsoft.EntityFrameworkCore;

namespace DBContext.Models
{
    [Keyless]
    public class SpecialTimeTollFee
    {
       
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int TollFee { get; set; }    
    }
}
