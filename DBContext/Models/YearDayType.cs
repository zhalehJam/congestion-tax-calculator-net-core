using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext.Models
{
    public class GetyearDayType
    { 
        [Key]
        public DateTime DateOfYear { get; set; }
        public bool IsfreeDate { get; set; }
    }
}
