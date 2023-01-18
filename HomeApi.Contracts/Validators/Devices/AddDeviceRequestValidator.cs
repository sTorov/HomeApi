using FluentValidation;
using HomeApi.Contracts.Models.Devices;

namespace HomeApi.Contracts.Validators.Devices
{
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        string[] _validLocation;

        /// <summary>
        /// Метод-конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            //Сохраним список доступных вариантов размещения устройств
            _validLocation = new[]
            {
                "Kitchen",
                "Bathroom",
                "Livingroom",
                "Toilet"
            };

            RuleFor(x => x.Name).NotEmpty();    //Проверка на null или пустое значение
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220);     //Проверяем, что значение находится в диапозоне
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.Location)
                .NotEmpty()
                .Must(BeSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", _validLocation)}");
        }

        /// <summary>
        /// Метод кастомной валидации для свойства Location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool BeSupported(string location)
        {
            return _validLocation.Any(e => e == location);
        }
    }
}
