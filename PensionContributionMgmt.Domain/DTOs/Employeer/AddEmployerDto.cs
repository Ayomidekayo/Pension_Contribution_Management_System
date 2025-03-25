

namespace PensionContributionMgmt.Domain.DTOs.Employeer
{
 public   class AddEmployerDto
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; }
        public Guid contributionId { get; set; }
        public Guid MemberId { get; set; }
    }
}
