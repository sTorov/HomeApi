using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Device;
using HomeApi.Contracts.Models.Home;
using HomeApi.Contracts.Models.Room;
using HomeApi.Data.Models;

namespace HomeApi.MappingProfiles
{
    /// <summary>
    /// Настройка маппинга всех сущностей приложения
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
