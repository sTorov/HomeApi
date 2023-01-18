using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        //Ссылка на объект когфигурации
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        //Инициализация кофигурации при вызове коструктора
        public HomeController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet]   //Для обслуживания Get-запросов
        [Route("info")]     //Настройка маршрута при помощи атрибутов
        public IActionResult Info()
        {
            //Получим запрос, "смапив" конфигурацию на модель запроса
            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);

            //Вернём ответ
            return StatusCode(200, infoResponse);
        }
    }
}
