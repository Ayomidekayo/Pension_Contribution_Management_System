using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.Entitie;
using PensionContributionMgmt.Infrastructure.Data;
using PensionContributionMgmt.Infrastructure.Service;

namespace PensionContributionMgmt.Infrastructure.Repository
{
    public class UnitOfwork : IUnitOfwork
    {
        private readonly AppDbContext appDbContext;
        private AppDbContext? _dbContext;

        public IContributionRepository Contribution { get; private set; }
        public IEmployerRepository Employer { get; private set; }
       public IGenericRepository<Member> Members { get; }
        public UnitOfwork(AppDbContext dbContext)
        {
            // Initialize repositories
            this._dbContext = dbContext;
            Contribution=new ContributionRepository(_dbContext);
            Employer=new EmployerRepository(_dbContext);
            Members =new  GenericRepository<Member>(_dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync(); // Commit transaction
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
