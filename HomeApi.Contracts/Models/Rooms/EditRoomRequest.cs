﻿namespace HomeApi.Contracts.Models.Rooms
{
    /// <summary>
    /// Модель запроса для обновления существующего устройства
    /// </summary>
    public class EditRoomRequest
    {
        public string? NewName { get; set; }
        public int? NewArea { get; set; }
        public bool? NewGasConnected { get; set; }
        public int? NewVoltage { get; set; }
    }
}
