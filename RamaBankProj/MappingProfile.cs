using AutoMapper;
using RamaBankProj.Dto;
using RamaBankProj.Model;

namespace RamaBankProj
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRequestModel, Account>()
                .ForMember(dest => dest.PhonePrimary, opt => opt.MapFrom(src => src.Phone));
        }
    }
}