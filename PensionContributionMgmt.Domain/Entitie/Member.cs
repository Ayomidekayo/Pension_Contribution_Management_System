using System.ComponentModel.DataAnnotations;
using PensionContributionMgmt.Domain.DTOs;


namespace PensionContributionMgmt.Domain.Entitie
{
  public  class Member
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// /
        /// </summary>
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<Contribution> Contributions { get; set; }
        public bool IsEligibleForBenefits { get; set; }

        public static ValidationResult ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--; 
                  //Adjust for birthday not yet reached

            return (age >= 18 && age <= 70)
                ? ValidationResult.Success
                : new ValidationResult("Member must be between 18 and 70 years old.");
        }

    }

}
