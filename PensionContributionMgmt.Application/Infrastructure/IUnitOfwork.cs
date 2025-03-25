

using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Application.Infrastructure
{
    public interface IUnitOfwork
    {
        IGenericRepository<Member> Members { get; }
        IContributionRepository Contribution { get; }
        IEmployerRepository Employer { get; }
        Task<int> CompleteAsync(); // Save changes and commit transaction


    }
}
