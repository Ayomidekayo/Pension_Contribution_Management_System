using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.Entitie;
using PensionContributionMgmt.Infrastructure.Data;

namespace PensionContributionMgmt.Infrastructure.Repository
{
    public class EmployerRepository : GenericRepository<Employer>, IEmployerRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployerRepository(AppDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Employer> GetmployerwithContributionAndMemberAsync(Guid id)
        {
           var employer=  await _dbContext.Employers.Include(x=>x.Contribution).Include(x=>x.Member).FirstOrDefaultAsync(x => x.Id == id);


            if (employer == null)
                return null;
            return employer;
        }
    }
}
