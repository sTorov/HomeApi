using HomeApi.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeApi.Data.Models
{
    /// <summary>
    /// Модель таблицы комнат
    /// </summary>
    [Table("Rooms")]
    public class Room : IClone<Room>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }

        public Room() { }

        private Room(Guid id, DateTime dateTime, string name, int area, bool gasConnected, int voltage)
        {
            Id = id;
            AddDate = dateTime;
            Name = name;
            Area = area;
            GasConnected = gasConnected;
            Voltage = voltage;
        }

        /// <summary>
        /// Клонирование объекта Room
        /// </summary>
        public Room Clone() => new (Id, AddDate, Name, Area, GasConnected, Voltage);            
    }
}
