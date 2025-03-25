

using System.ComponentModel.DataAnnotations;

namespace PensionContributionMgmt.Domain.DTOs
{
  public  class MemberDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        //  [CustomValidation(typeof(Member))]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
