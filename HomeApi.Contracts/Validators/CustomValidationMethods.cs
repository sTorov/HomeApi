namespace HomeApi.Contracts.Validators
{
    /// <summary>
    /// Класс-контейнер для хранения кастомных методов валидации
    /// </summary>
    public class CustomValidationMethods
    {
        /// <summary>
        /// Метод кастомной валидации для сравнения полученного значения со списком допустимых значений ValidRooms
        /// </summary>
        public static bool BeSupported(string? location) =>
            Values.ValidRooms.Any(e => e == location);

        /// <summary>
        /// Метод кастомной валидации для сравнения полученного значения со списком допустимых значений ValidRooms (допускается значение null)
        /// </summary>
        public static bool BeNullableSupported(string? location)
        {
            if (location == null) return true;
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
