using FluentValidation;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validators.Room
{
    /// <summary>
    /// Класс-валидатор запросов на добавление новой комнаты
    /// </summary>
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator()
        {
            RuleFor(x => x.Area).GreaterThan(0)
                .WithMessage($"Площадь помещения должна быть больше нуля!");
            RuleFor(x => x.Name).Must(BeSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.Voltage).GreaterThanOrEqualTo(0)
                .WithMessage($"Вольтаж должен быть больше либо равен нулю!");
        }

        /// <summary>
        /// Метод кастомной валидации для свойства Name
        /// </summary>
        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
