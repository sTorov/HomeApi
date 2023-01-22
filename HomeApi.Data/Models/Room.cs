﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HomeApi.Data.Models
{
    /// <summary>
    /// Модель таблицы комнат
    /// </summary>
    [Table("Rooms")]
    public class Room
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}
