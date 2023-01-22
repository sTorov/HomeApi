using FluentValidation;
using HomeApi.Contracts.Models.Room;

namespace HomeApi.Contracts.Validators.Room
{
    /// <summary>
    /// Класс-валидатор запросов добавление новой комнаты
    /// </summary>
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        /// <summary>
        /// Метод-конструктор, устанавливающий правила
        /// </summary>

        public AddRoomRequestValidator()
        {
            RuleFor(x => x.Area).NotEmpty().GreaterThan(0)
                .WithMessage($"Площадь помещения должна быть больше нуля!");
            RuleFor(x => x.Name).NotEmpty().Must(CustomValidationMethods.BeSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.Voltage).NotEmpty().GreaterThanOrEqualTo(0)
                .WithMessage($"Вольтаж должен быть больше либо равен нулю!");
            RuleFor(x => x.GasConnected).NotNull();
        }
    }
}
