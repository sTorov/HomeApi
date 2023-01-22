namespace HomeApi.Contracts.Models.Device
{
    /// <summary>
    /// Модель ответа для удаления устройства
    /// </summary>
    public class DeleteDeviceResponse
    {
        public string Message { get; set; }
        public DeviceView DeletedDevice { get; set; }
    }
}
