namespace PensionContributionMgmt.Domain.DTOs
{
    public class EmployerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
