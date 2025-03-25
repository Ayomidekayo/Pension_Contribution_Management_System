
using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
    }
}