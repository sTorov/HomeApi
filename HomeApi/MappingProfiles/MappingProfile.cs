using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Home;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;

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
                .ForMember(m => m.AddressInfo, opt => opt.MapFrom(src => src.Address));

            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location, opt => opt.MapFrom(r => r.RoomLocation));

            CreateMap<Device, DeviceView>();

            CreateMap<AddRoomRequest, Room>();

            CreateMap<Room, RoomView>();
        }
    }
}
