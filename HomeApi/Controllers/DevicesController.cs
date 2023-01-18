using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Devices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<DevicesController> _logger;
        private readonly IOptions<HomeOptions> _options;
        private readonly IMapper _mapper;

        public DevicesController(IHostEnvironment env, 
            ILogger<DevicesController> logger,
            IOptions<HomeOptions> options,
            IMapper mapper)
        {
            _env = env;
            _logger = logger;
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод для скачивания файла-руководства, либо получение информации о файле-руководстве
        /// </summary>
        [HttpGet]
        [HttpHead]
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer)     //[FromRoute] - из параметров метода взято значение переменной для указания пути к методу
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
        public IActionResult Get()
        {
            return StatusCode(200, "Устройства отсутствуют!");
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AddDeviceRequest request)   //[FromBody] - Атрибут, указывающий, откуда брать значение объекта 
        {
            //if(request.CurrentVolts < 120)
            //    return StatusCode(403, $"Устройства с напряжением менее 120 вольт не поддерживаются!");

            //Добавляем для клиента информативую ошибку
            if (request.CurrentVolts < 120)
            {
                //Содержит информацию о состоянии объекта
                ModelState.AddModelError("currentVolts", "Устройства с напряжением менее 120 вольт не поддерживаются!");
                return BadRequest(ModelState);
            }

            return StatusCode(200, $"Устройство {request.Name} добавлено!");
        }
    }
}
