using System.ComponentModel.DataAnnotations;

namespace PensionContributionMgmt.Domain.DTOs.Contribution
{
 public   class AddContributioDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EmployerId { get; set; }

        [Required(ErrorMessage = "Contribution amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Contribution amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Contribution date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [CustomValidation(typeof(ContributionDto), nameof(ValidateContributionDate))]
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
