

namespace PensionContributionMgmt.Domain.DTOs.Employeer
{
 public   class AddEmployerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
