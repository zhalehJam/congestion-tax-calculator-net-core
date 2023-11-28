using DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DBContext
{
    public partial class TollCalculationDBContext : DbContext
    {
        public virtual DbSet<GetyearDayType> GetyearDayTypes { get; set; } = null!;
        public virtual DbSet<SpecialTimeTollFee> SpecialTimeTollFees { get; set; }

        public TollCalculationDBContext()
        {

        }
        public TollCalculationDBContext(DbContextOptions<TollCalculationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer("Server =.,1433; Database = TollFeeDB; user id=sa;password=123; TrustServerCertificate=True");
            }
        }


    }
}
