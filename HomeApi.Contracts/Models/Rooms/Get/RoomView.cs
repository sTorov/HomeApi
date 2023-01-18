namespace HomeApi.Contracts.Models.Rooms.Get
{
    public class RoomView
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}
