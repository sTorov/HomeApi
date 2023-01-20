namespace HomeApi.Contracts.Models.Rooms
{
    public class GetRoomResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}
