using FluentValidation;
using HomeApi.Contracts.Models.Room;

namespace HomeApi.Contracts.Validators.Room
{
    /// <summary>
    /// Класс-валидатор запросов обновление комнаты
    /// </summary>

    public class EditRoomRequestValidation : AbstractValidator<EditRoomRequest>
    {
        /// <summary>
        /// Метод-конструктор, устанавливающий правила
        /// </summary>

        public EditRoomRequestValidation()
        {
            RuleFor(x => x.NewArea).GreaterThan(0)
                .WithMessage($"Площадь помещения должна быть больше нуля!");
            RuleFor(x => x.NewVoltage).GreaterThanOrEqualTo(0)
                .WithMessage($"Вольтаж должен иметь положительное либо нулевое значение!");
            RuleFor(x => x.NewName).Must(CustomValidationMethods.BeNullableSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
        }
    }
}
