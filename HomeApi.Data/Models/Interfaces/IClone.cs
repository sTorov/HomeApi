namespace HomeApi.Data.Models.Interfaces
{
    /// <summary>
    /// Интерфейс, обязывающий реализовать метод клонирования объекта
    /// </summary>
    public interface IClone<T>
    {
        public T Clone();
    }
}
