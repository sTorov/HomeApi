using FluentValidation;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validators.Room
{
    public class EditRoomRequestValidation : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidation()
        {
            RuleFor(x => x.NewArea).GreaterThan(0)
                .WithMessage($"Площадь помещения должна быть больше нуля!");
            RuleFor(x => x.NewVoltage).GreaterThanOrEqualTo(0)
                .WithMessage($"Вольтаж должен иметь положительное либо нулевое значение!");
            RuleFor(x => x.NewName).Must(BeSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
        }

        /// <summary>
        /// Метод кастомной валидации для свойства location
        /// </summary>
        private bool BeSupported(string location)
        {
            if(location == null) return true;
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
