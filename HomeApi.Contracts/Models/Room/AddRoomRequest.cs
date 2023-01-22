namespace HomeApi.Contracts.Models.Room
{
    /// <summary>
    /// Модель запроса для добавления новой комнаты
    /// </summary>
    public class AddRoomRequest
    {
        public string? Name { get; set; }
        public int? Area { get; set; }
        public bool? GasConnected { get; set; }
        public int? Voltage { get; set; }
    }
}
