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

       
    }
}
