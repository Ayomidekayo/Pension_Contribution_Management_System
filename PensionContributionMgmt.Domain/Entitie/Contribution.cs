

using System.ComponentModel.DataAnnotations;
using PensionContributionMgmt.Domain.DTOs;

namespace PensionContributionMgmt.Domain.Entitie
{
  public  class Contribution
    {
        [Key]
       
        public Guid Id { get; set; } 
        public Guid MemberId { get; set; } 
        public decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsVoluntary { get; set; }
        public string ContributionType { get; set; }

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
