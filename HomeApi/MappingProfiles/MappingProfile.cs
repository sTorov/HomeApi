using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Home;

namespace HomeApi.MappingProfiles
{
    /// <summary>
    /// Настройка моппинга всех сущностей приложения
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));
        }
    }
}
