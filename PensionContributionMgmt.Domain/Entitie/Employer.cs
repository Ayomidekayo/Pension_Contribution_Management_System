
using System.ComponentModel.DataAnnotations;

namespace PensionContributionMgmt.Domain.Entitie
{
  public  class Employer
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Company name must be between 3 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Company name can only contain letters, numbers, and spaces.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Registration number is required.")]
        [RegularExpression(@"^[A-Z0-9]{8,15}$", ErrorMessage = "Registration number must be 8-15 alphanumeric characters.")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Active status is required.")]
        public bool IsActive { get; set; }
        public Guid contributionId { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Contribution Contribution { get; set; }
    }
}
