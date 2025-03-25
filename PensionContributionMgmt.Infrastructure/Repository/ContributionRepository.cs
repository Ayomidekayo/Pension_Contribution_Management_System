
using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.Entitie;
using PensionContributionMgmt.Infrastructure.Data;

namespace PensionContributionMgmt.Infrastructure.Repository
{
    public class ContributionRepository : GenericRepository<Contribution>, IContributionRepository
    {
        private readonly AppDbContext _dbContext;

        public ContributionRepository(AppDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<decimal> GetTotalContributionsAsync(int memberId)
        {

            return await _dbContext.Contributions.Where(c => c.MemberId == memberId).SumAsync(c => c.Amount);
        }

        public async Task<bool> HasMonthlyContribution(int memberId, DateTime date)
        {
         return   await _dbContext.Contributions.AnyAsync(c => c.MemberId == memberId && c.ContributionDate.Year == date.Year && c.ContributionDate.Month == date.Month && !c.IsVoluntary);
        }
    }
}
