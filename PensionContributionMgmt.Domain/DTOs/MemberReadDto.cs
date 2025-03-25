

namespace PensionContributionMgmt.Domain.DTOs
{
 public   class MemberReadDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
