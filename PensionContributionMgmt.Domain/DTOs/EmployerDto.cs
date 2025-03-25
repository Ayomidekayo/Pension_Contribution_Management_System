namespace PensionContributionMgmt.Domain.DTOs
{
    public class EmployerDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsActive { get; set; }
        public Guid contributionId { get; set; }
        public Guid MemberId { get; set; }
        public MemberDto Member { get; set; }
        public ContributionDto Contribution { get; set; }
    }
}
