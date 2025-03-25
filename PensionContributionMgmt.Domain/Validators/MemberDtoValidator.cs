using FluentValidation;
using PensionContributionMgmt.Domain.DTOs;
namespace PensionContributionMgmt.Domain.Validators
{
    
    public class MemberDtoValidator : AbstractValidator<MemberDto>
    {
        public MemberDtoValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(m => m.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .Must(BeValidAge).WithMessage("Age must be between 18 and 70 years.");

            RuleFor(m => m.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(m => m.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");
        }

        private bool BeValidAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--; // Adjust for birthdate not yet reached in the current year
            return age >= 18 && age <= 70;
        }
    }

}
