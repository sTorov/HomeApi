namespace HomeApi.Contracts.Models.Devices.Get
{
    public class GetDeviceResponse
    {
        public int DeviceAmount { get; set; }
        public DeviceView[] Devices { get; set; }
    }
}
