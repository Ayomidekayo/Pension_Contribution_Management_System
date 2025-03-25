using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Domain.DTOs;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Infrastructure.Service
{
  public  class MemberService :IMemberService
    {
       // private readonly IGenericRepository<Member> _memberRepository;
        private readonly IUnitOfwork _unitOfwork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfwork unitOfwork, IMapper mapper)
        {
           // _memberRepository = memberRepository;
            this._unitOfwork = unitOfwork;
            _mapper = mapper;
        }
        public async Task<bool> CreateUserAsync(MemberRegistrationDto dto)
        {
           
            ArgumentNullException.ThrowIfNull(dto, $"the argument {nameof(dto)} is null");

            var existingUser = await _unitOfwork.Members.GetAsync(u => u.Name.Equals(dto.Name));

            if (existingUser != null)
            {
               throw new Exception("The name already taken");
            }

            Member user = _mapper.Map<Member>(dto);
            user.IsDeleted = false;
            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHashWithSalt(dto.Password);
                user.Password = passwordHash.PasswordHash;
                user.PasswordSalt = passwordHash.Salt;
            }

            await _unitOfwork.Members.AddAsync(user);

            return true;
        }

        public async Task<List<MemberReadDto>> GetUsersAsync()
        {
            var users = await _unitOfwork.Members.GetAllByFilterAsync(u => !u.IsDeleted);

            return _mapper.Map<List<MemberReadDto>>(users);
        }

        public async Task<MemberReadDto> GetUserByIdAsync(int id)
        {
            var user = await _unitOfwork.Members.GetAsync(u => !u.IsDeleted && u.Id == id);

            return _mapper.Map<MemberReadDto>(user);
        }

        public async Task<MemberReadDto> GetUserByUsernameAsync(string username)
        {
            var user = await _unitOfwork.Members.GetAsync(u => !u.IsDeleted && u.Name.Equals(username));

            return _mapper.Map<MemberReadDto>(user);
        }

        public async Task<bool> UpdateUserAsync(MemberDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            var existingUser = await _unitOfwork.Members.GetAsync(u => !u.IsDeleted && u.Id == dto.Id, true);
            if (existingUser == null)
            {
                throw new Exception($"User not found with the Id: {dto.Id}");
            }

            var userToUpdate = _mapper.Map<Member>(dto);
            userToUpdate.ModifiedDate = DateTime.Now;

            //1. We will update only the user information
            //2. We need to provide separate method to update the password
            //for the demo purpose I am updating the password also
            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHashWithSalt(dto.Password);
                userToUpdate.Password = passwordHash.PasswordHash;
                userToUpdate.PasswordSalt = passwordHash.Salt;
            }

            await _unitOfwork.Members.UpdateAsync(userToUpdate);

            return true;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            if (userId == 0)
                throw new ArgumentException(nameof(userId));

            var existingUser = await _unitOfwork.Members.GetAsync(u => !u.IsDeleted && u.Id == userId, true);
            if (existingUser == null)
            {
                throw new Exception($"User not found with the Id: {userId}");
            } 
            /// Soft delete 

            existingUser.IsDeleted = true;

            await _unitOfwork.Members.UpdateAsync(existingUser);

            return true;
        }
        public (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password)
        {
            //Create the salt
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //Create Password Hash
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            return (hash, Convert.ToBase64String(salt));
        }
    }
}

