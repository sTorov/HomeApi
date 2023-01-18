using System.ComponentModel.DataAnnotations;

namespace HomeApi.Contracts.Models.Devices
{
    /// <summary>
    /// Модель запроса для добавления нового устройства
    /// </summary>
    public class AddDeviceRequest
    {
        //[Required]  //Обозначаем поля, как обязательные для заполнения
        public string Name { get; set; }
        //[Required]
        public string Manufacturer { get; set; }
        //[Required]
        public string Model { get; set; }
        //[Required]
        public string SerialNumber { get; set; }
        //[Range(120, 220, ErrorMessage = "Поддерживаются устройства с напряжением от {1} до {2} вольт")] //Указываем диапозон значений и текст ошибки
        public int CurrentVolts { get; set; }
        //[Required]
        public bool GasUsage { get; set; }
        //[Required]
        public string RoomLocation { get; set; }
    }
}
