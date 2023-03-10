using AutoMapper;
using HomeApi.Contracts.Models.Device;
using HomeApi.Data.Queries;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IDeviceRepository _devices;
        private readonly IRoomRepository _rooms;

        public DeviceController(IHostEnvironment env, IMapper mapper, IDeviceRepository devices, IRoomRepository rooms)
        {
            _env = env;
            _mapper = mapper;
            _devices = devices;
            _rooms = rooms;
        }

        /// <summary>
        /// Метод для скачивания файла-руководства, либо получение информации о файле-руководстве
        /// </summary>
        [HttpGet]
        [HttpHead]
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer)
        {
            string staticPath = Path.Combine(_env.ContentRootPath, "Static");
            string filePath = Directory.GetFiles(staticPath)
                .FirstOrDefault(f => f.Split("\\")
                .Last()
                .Split(".")[0] == manufacturer)!;

            if(string.IsNullOrEmpty(filePath))
                return StatusCode(404, $"Инструкция для производителя {manufacturer} не найдена на сервере. Проверте название!");

            string fileType = "application/pdf";
            string fileName = $"{manufacturer}.pdf";

            return PhysicalFile(filePath, fileType, fileName);
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevices();

            var resp = new GetDeviceResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomByName(request.RoomLocation);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.RoomLocation} не подключена. Сначала подключите комнату!");

            var device = await _devices.GetDeviceByName(request.Name);
            if (device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует!");

            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDevice(newDevice, room);

            return StatusCode(201, $"Устройство {request.Name} добавлено. Идентификатор: {newDevice.Id}");
        }

        /// <summary>
        /// Обновление сущетвующего устройства
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody] EditDeviceRequest request)
        {
            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(400, $"Ошибка: Устройство с идентификатором {id} не существует!");

            request.NewRoom ??= device.Location;

            var room = await _rooms.GetRoomByName(request.NewRoom);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {request.NewRoom} не подключена. Сначала подключите комнату!");            

            var withSameName = await _devices.GetDeviceByName(request.NewName);
            if (withSameName != null)
                return StatusCode(400, $"Ошибка: Устройство с именем {request.NewName} уже подключено. Выберете другое имя!");

            await _devices.UpdateDevice(device, room, 
                new UpdateDeviceQuery(request.NewName, request.NewSerial));

            return StatusCode(200, $"Устройство обновлено! Имя - {device.Name}, Серийный номер - {device.SerialNumber},  Комната подключения - {device.Room.Name}");
        }

        /// <summary>
        /// Удаление существующего устройства
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var device = await _devices.GetDeviceById(id);
            if (device == null)
                return StatusCode(404, $"Ошибка: Не удалось найти устройство с идентификатором {id} для удаления!");

            await _devices.DeleteDevice(device);

            var resp = new DeleteDeviceResponse
            {
                Message = $"Операция удаления проведена успешно.",
                DeletedDevice = _mapper.Map<DeviceView>(device)
            };

            return StatusCode(200, resp);
        }
    }
}
