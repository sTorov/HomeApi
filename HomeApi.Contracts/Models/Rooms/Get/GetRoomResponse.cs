namespace HomeApi.Contracts.Models.Rooms.Get
{
    public class GetRoomResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}
