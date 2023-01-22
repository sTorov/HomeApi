namespace HomeApi.Contracts.Models.Room
{
    /// <summary>
    /// Модель ответа для обновления комнаты
    /// </summary>
    public class EditRoomResponse
    {
        public string Message { get; set; }
        public RoomView OldRoom { get; set; }
        public RoomView NewRoom { get; set; }
    }
}
