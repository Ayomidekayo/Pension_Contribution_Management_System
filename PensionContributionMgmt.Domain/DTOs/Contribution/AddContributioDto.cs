using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionContributionMgmt.Domain.DTOs.Contribution
{
 public   class AddContributioDto
    {

        [Required(ErrorMessage = "Contribution amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Contribution amount must be greater than zero.")]
        public decimal Amount { get; set; }
       
        [Required(ErrorMessage = "Member ID is required.")]
        // [Range(1, ErrorMessage = "Invalid Member ID.")]
        public Guid MemberId { get; set; }


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
