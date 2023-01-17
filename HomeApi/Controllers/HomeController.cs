using HomeApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        //Ссылка на объект когфигурации
        private IOptions<HomeOptions> _options;

        //Инициализация кофигурации при вызове коструктора
        public HomeController(IOptions<HomeOptions> options)
        {
            _options = options;
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet]   //Для обслуживания Get-запросов
        [Route("info")]     //Настройка маршрута при помощи атрибутов
        public IActionResult Info()
        {
            //Объект StringBuilder, в котором будем собирать результат из конфигурации
            var pageResult = new StringBuilder();

            //Добавляем все значения конфигурации для последующего вывода на страницу
            pageResult.Append($"Добро пожаловать в API вашего дома!\n");
            pageResult.Append($"Здесь вы можете посмотреть основную информацию.\n\n");
            pageResult.Append($"Количество этажей: {_options.Value.FloorAmount}\n");
            pageResult.Append($"Стационарный телефон: {_options.Value.Telephone}\n");
            pageResult.Append($"Тип отопления: {_options.Value.Heating}\n");
            pageResult.Append($"Напряжение электросети: {_options.Value.CurrentVolts}\n");
            pageResult.Append($"Подключен к газовой сети: {_options.Value.GasConnected}\n");
            pageResult.Append($"Жилая площадь: {_options.Value.Area} m2\n");
            pageResult.Append($"Материал: {_options.Value.Material}\n\n");
            pageResult.Append($"Адрес:      {_options.Value.Address.Street} {_options.Value.Address.House}/{_options.Value.Address.Building}\n");

            //Преобразуем результат в строку и выводим, как обычнею веб-страницу
            return StatusCode(200, pageResult.ToString());
        }
    }
}
