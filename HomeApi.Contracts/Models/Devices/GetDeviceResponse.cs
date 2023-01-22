namespace HomeApi.Contracts.Models.Devices
{
    /// <summary>
    /// Получения всех подключеных устройств (модель ответа)
    /// </summary>
    public class GetDeviceResponse
    {
        public int DeviceAmount { get; set; }
        public DeviceView[] Devices { get; set; }
    }
}
