using Hangfire;

namespace PensionContributionMgmt.Domain.Entitie
{
    public class BackgroundJobs
    {
        public BackgroundJobs()
        {

        }
        //public BackgroundJobs(icontributionRepository) {  }

        [AutomaticRetry(Attempts = 3)]
        public async Task ValidateContributions()
        {
            // members = await _repository.GetByMemberIdAsync(Guid.Empty);
            Console.WriteLine("Validating contributions...");
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task GenerateBenefitEligibilityUpdates()
        {
            Console.WriteLine("Checking benefit eligibility...");
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task HandleFailedTransactions()
        {
            Console.WriteLine("Handling failed transactions...");
        }
    }
}