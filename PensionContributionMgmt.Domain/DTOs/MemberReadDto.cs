

namespace PensionContributionMgmt.Domain.DTOs
{
 public   class MemberReadDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public List<ContributionDto> Contributions { get; set; }
    }
}
