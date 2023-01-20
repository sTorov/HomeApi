using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Home;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;

namespace HomeApi.MappingProfiles
{
    /// <summary>
    /// Настройка моппинга всех сущностей приложения
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();//.ReverseMap() -> для возможности обратного преобразования
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo, opt => opt.MapFrom(src => src.Address));

            //Валидация
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location, opt => opt.MapFrom(r => r.RoomLocation));   //Необходимо вызывать для маппинга каждого разноименного свойства
                //.ForMember(d => d.Name, opt => opt.MapFrom...)
            CreateMap<Device, DeviceView>();
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Room, RoomView>();
        }
    }
}
