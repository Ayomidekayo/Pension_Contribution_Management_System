
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Application.Infrastructure
{
  public  interface IEmployerRepository:IGenericRepository<Employer>
    {
        Task<Employer> GetmployerwithContributionAndMemberAsync(Guid id);
    }
}
