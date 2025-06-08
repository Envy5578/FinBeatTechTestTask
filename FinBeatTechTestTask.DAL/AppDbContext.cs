using FinBeatTechTestTask.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinBeatTechTestTask.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        internal DbSet<PairValueEntity> pairValues { get; set; }
    }
}
