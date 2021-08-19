using AutoMapper;
using DataAccess.Models;
using Models.Provider;
using Models.General;
using Models.Member;

namespace DataAccess
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Member, MemberViewModel>()
                .ReverseMap();

            CreateMap<Provider, ProviderViewModel>()
              .ReverseMap();

            CreateMap<ProviderSpeciality, ProviderSpecialtyViewModel>()
             .ReverseMap();

            CreateMap<Speciality, SpecialtyViewModel>()
                .ReverseMap();

        }
    }
}
