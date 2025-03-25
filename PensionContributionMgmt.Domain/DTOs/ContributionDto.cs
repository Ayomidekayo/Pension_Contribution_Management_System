

using System.ComponentModel.DataAnnotations;

namespace PensionContributionMgmt.Domain.DTOs
{
 public  class ContributionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsVoluntary { get; set; }
        [Required(ErrorMessage = "Member ID is required.")]
       // [Range(1, ErrorMessage = "Invalid Member ID.")]
        public Guid MemberId { get; set; }

        [Required(ErrorMessage = "Employer ID is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid Employer ID.")]
        public int EmployerId { get; set; }

        [Required(ErrorMessage = "Contribution amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Contribution amount must be greater than zero.")]
        public decimal ContributionAmount { get; set; }

        [Required(ErrorMessage = "Contribution date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [CustomValidation(typeof(ContributionDto), nameof(ValidateContributionDate))]
        public DateTime ContributionDate { get; set; }

        [Required(ErrorMessage = "Contribution Type is required.")]
        [RegularExpression("^(Monthly|Voluntary)$", ErrorMessage = "Contribution Type must be 'Monthly' or 'Voluntary'.")]
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
