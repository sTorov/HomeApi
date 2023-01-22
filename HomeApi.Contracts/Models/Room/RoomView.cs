namespace HomeApi.Contracts.Models.Room
{
    /// <summary>
    /// Модель комнаты (для модель ответа)
    /// </summary>
    public class RoomView
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}
