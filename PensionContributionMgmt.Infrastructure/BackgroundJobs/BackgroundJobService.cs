using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PensionContributionMgmt.Domain.Entitie;
using PensionContributionMgmt.Infrastructure.Data;
using PensionContributionMgmt.Infrastructure.Service;

public class BackgroundJobService
{
    private readonly AppDbContext _context;
    private readonly ILogger<BackgroundJobService> _logger;
    private readonly INotificationService _notificationService;

    public BackgroundJobService(AppDbContext context, ILogger<BackgroundJobService> logger, INotificationService notificationService)
    {
        _context = context;
        _logger = logger;
        _notificationService = notificationService;
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task ValidateContributionsAsync()
    {
        try
        {
            _logger.LogInformation("Starting contribution validation job...");

            var invalidContributions = await _context.Contributions
                .Where(c => c.Amount <= 0 || c.MemberId == null || c.EmployerId == null)
                .ToListAsync();

            foreach (var contribution in invalidContributions)
            {
                _logger.LogWarning($"Invalid contribution detected: ID={contribution.Id}, Amount={contribution.Amount}");
                contribution.Status = "Invalid";
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Contribution validation completed. {invalidContributions.Count} invalid contributions found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while validating contributions.");
            throw;
        }
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task GenerateBenefitUpdatesAsync()
    {
        try
        {
            _logger.LogInformation("Starting benefit update job...");

            var eligibleMembers = await _context.Members
                .Where(m => m.IsEligibleForBenefits && m.Contributions.Any())
                .Include(m => m.Contributions)
                .ToListAsync();

            foreach (var member in eligibleMembers)
            {
                decimal totalContributions = member.Contributions.Sum(c => c.Amount);
                decimal interest = totalContributions * 0.05m; // 5% interest rate

                var benefitUpdate = new Benefit
                {
                    MemberId = member.Id,
                    AccruedInterest = interest,
                    TotalBalance = totalContributions + interest,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Benefits.Add(benefitUpdate);
                _logger.LogInformation($"Benefit updated for Member {member.Id}: Total Balance={benefitUpdate.TotalBalance}");
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Benefit update job completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while generating benefit updates.");
            throw;
        }
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task HandleFailedTransactionsAsync()
    {
        try
        {
            _logger.LogInformation("Starting failed transaction handling job...");

            var failedTransactions = await _context.Transactions
                .Where(t => t.Status == "Failed")
                .ToListAsync();

            foreach (var transaction in failedTransactions)
            {
                _logger.LogWarning($"Handling failed transaction: ID={transaction.Id}, Amount={transaction.Amount}");

                // Retry logic: Attempt to reprocess the transaction
                transaction.RetryCount++;

                if (transaction.RetryCount >= 3)
                {
                    transaction.Status = "Final Failed";
                    await _notificationService.SendNotificationAsync(transaction.MemberId,
                        "Your transaction has permanently failed. Please contact support.");
                }
                else
                {
                    transaction.Status = "Pending Retry";
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Failed transaction handling job completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling failed transactions.");
            throw;
        }
    }
}
