using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(IHostEnvironment env, ILogger<DevicesController> logger)
        {
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        [HttpHead]
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer)     //из параметров метода взято значение переменной для указания пути к методу
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
    }
}
