using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;

        public RoomRepository(HomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if(entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Найти комнату по id
        /// </summary>
        public async Task<Room> GetRoomById(Guid id)
        {
            return await _context.Rooms.Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Обновление комнаты
        /// </summary>
        /// <param name="room"></param>
        public async Task UpdateRoom(Room room, UpdateRoomQuery query)
        {
            if(!string.IsNullOrEmpty(query.NewName))
                room.Name = query.NewName;
            if(query.NewGasConnected != null)
                room.GasConnected = (bool)query.NewGasConnected;
            if(query.NewArea != null)
                room.Area = (int)query.NewArea;
            if(query.NewVoltage != null)
                room.Voltage = (int)query.NewVoltage;

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }
    }
}
