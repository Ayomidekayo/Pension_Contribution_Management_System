
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Application.Infrastructure
{
public interface IContributionRepository :IGenericRepository<Contribution>
    {
      

        //Task AddAsync(Contribution contribution);
        //Task<List<Contribution>> GetByMemberIdAsync(Guid memberId);
        Task<decimal> GetTotalContributionsAsync(Guid memberId);
        Task<bool> HasMonthlyContribution(Guid memberId, DateTime date);
    }
}
