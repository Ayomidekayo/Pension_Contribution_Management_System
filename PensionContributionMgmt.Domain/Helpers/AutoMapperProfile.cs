
using AutoMapper;
using PensionContributionMgmt.Domain.DTOs;
using PensionContributionMgmt.Domain.DTOs.Contribution;
using PensionContributionMgmt.Domain.DTOs.Employeer;
using PensionContributionMgmt.Domain.Entitie;

namespace PensionContributionMgmt.Domain.Helpers
{
  public  class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Member,MemberDto>().ReverseMap();
            CreateMap<Member,MemberReadDto>().ReverseMap();
            CreateMap<Member,MemberRegistrationDto>().ReverseMap();
            CreateMap<Employer, AddEmployerDto>().ReverseMap();
            CreateMap<Employer, EmployerDto>().ReverseMap();
            CreateMap<Contribution, ContributionDto>().ReverseMap();
            CreateMap<Contribution, AddContributioDto>().ReverseMap();
        }
    }
}
