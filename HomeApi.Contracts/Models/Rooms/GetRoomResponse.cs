namespace HomeApi.Contracts.Models.Rooms
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
