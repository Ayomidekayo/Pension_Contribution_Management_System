

using System.ComponentModel.DataAnnotations;
using PensionContributionMgmt.Utility;
using static PensionContributionMgmt.Utility.SD;
namespace PensionContributionMgmt.Domain.Entitie
{
  public  class Contribution
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EmployerId { get; set; }
        public decimal Amount { get; set; }
        public bool IsVoluntary { get; set; }
        public ContributionType ContributionType { get; set; } = SD.ContributionType.Monthly; // "Monthly" or "Voluntary"
        public string Status { get; set; } = "Active"; // "Monthly" or "Voluntary"
        public DateTime ContributionDate { get; set; }

       
        public static ValidationResult ValidateContributionDate(DateTime date, ValidationContext context)
        {
            DateTime today = DateTime.Today;

            if (date > today)
                return new ValidationResult("Contribution date cannot be in the future.");

            if (date < today.AddYears(-5)) // Example: Restrict to last 5 years
                return new ValidationResult("Contribution date cannot be older than 5 years.");

            return ValidationResult.Success;
        }
    }
}
