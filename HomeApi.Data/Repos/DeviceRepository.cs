using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Device" в базе
    /// </summary>
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HomeApiContext _context;

        public DeviceRepository(HomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Удалить устройство
        /// </summary>
        public async Task DeleteDevice(Device device)
        {
            var entry = _context.Entry(device);
            if(entry.State == EntityState.Unchanged)
                _context.Devices.Remove(device);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Найти устройство по идентификатору
        /// </summary>
        public async Task<Device> GetDeviceById(Guid id)
        {
            return await _context.Devices
                 .Include(d => d.Room)
                 .Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Найти устройство по имени
        /// </summary>
        public async Task<Device> GetDeviceByName(string name)
        {
            return await _context.Devices
                .Include(d => d.Room)
                .Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Выгрузить все устройства
        /// </summary>
        public async Task<Device[]> GetDevices()
        {
            return await _context.Devices
                .Include(d => d.Room).ToArrayAsync();
        }

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        public async Task SaveDevice(Device device, Room room)
        {
            device.RoomId = room.Id;
            device.Room = room;

            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                await _context.Devices.AddAsync(device);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить существующее устройство
        /// </summary>
        public async Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query)
        {
            if (room.Id != device.RoomId)
            {
                device.RoomId = room.Id;
                device.Location = room.Name;
                device.Room = room;
            }

            if (!string.IsNullOrEmpty(query.NewName))
                device.Name = query.NewName;
            if (!string.IsNullOrEmpty(query.NewSerial))
                device.SerialNumber = query.NewSerial;

            var entry = _context.Entry(device);
            if (entry.State == EntityState.Detached)
                _context.Devices.Update(device);

            await _context.SaveChangesAsync();
        }
    }
}
