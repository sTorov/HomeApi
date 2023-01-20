using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private IRoomRepository _repo;
        private IMapper _mapper;

        public RoomController(IRoomRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех существующих комнат
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repo.GetAllRooms();
            var resp = new GetRoomResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var exitingRoom = await _repo.GetRoomByName(request.Name);
            if (exitingRoom == null) 
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repo.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        /// <summary>
        /// Обновление данных о существующей комнате
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] EditRoomRequest request
            )
        {
            var room = await _repo.GetRoomById(id);
            if (room == null)
                return StatusCode(404, $"Ошибка: Неудалось найти комнату с идентификатором {id}");

            var withSameName = await _repo.GetRoomByName(request.NewName);
            if (withSameName != null)
                return StatusCode(400, $"Комната с именем {request.NewName} уже существует. Выберете другое имя!");

            var query = new UpdateRoomQuery(
                newGasConnected: request.NewGasConnected,
                newName: request.NewName,
                newArea: request.NewArea,
                newVoltage: request.NewVoltage
                );

            await _repo.UpdateRoom(room, query);
            return StatusCode(200, $"Информация о комнате с идентификатором {id} успешно обновлена!");
        }
    }
}
