namespace HomeApi.Contracts.Home
{
    /// <summary>
    /// Адрес дома (для модели ответа)
    /// </summary>
    public class AddressInfo
    {
        public int House { get; set; }
        public int Building { get; set; }
        public string Street { get; set; }
    }
}
