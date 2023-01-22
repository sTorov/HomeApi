namespace HomeApi.Contracts.Models.Room
{
    /// <summary>
    /// Получение списка всех существующих комнат (модель ответа)
    /// </summary>
    public class GetRoomResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}
