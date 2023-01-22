using FluentValidation;
using HomeApi.Contracts.Models.Device;

namespace HomeApi.Contracts.Validators.Device
{
    /// <summary>
    /// Класс-валидатор запросов подключения новых устройств
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        /// <summary>
        /// Метод-конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.RoomLocation)
                .NotEmpty()
                .Must(CustomValidationMethods.BeSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
        }
    }
}
