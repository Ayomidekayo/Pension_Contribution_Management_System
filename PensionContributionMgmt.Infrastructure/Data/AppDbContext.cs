
using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Employer> Employers { get; set; }
    }
}
