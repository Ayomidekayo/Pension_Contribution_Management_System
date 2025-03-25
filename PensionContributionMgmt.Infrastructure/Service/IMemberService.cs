
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.DTOs;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Infrastructure.Service
{
  public  interface IMemberService
    {
        Task<bool> CreateUserAsync(MemberRegistrationDto dto);
        Task<List<MemberReadDto>> GetUsersAsync();
        Task<MemberReadDto> GetUserByIdAsync(Guid id);
        Task<MemberReadDto> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(MemberDto dto);
        Task<bool> DeleteUser(Guid userId);
        (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password);
    }
}
