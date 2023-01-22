using FluentValidation;
using HomeApi.Contracts.Models.Devices;

namespace HomeApi.Contracts.Validators.Device
{
    /// <summary>
    /// Класс-валидатор запросов обновления устройства
    /// </summary>
    public class EditDeviceRequestValidator : AbstractValidator<EditDeviceRequest>
    {
        /// <summary>
        /// Метод-конструктор, установливающий правила
        /// </summary>
        public EditDeviceRequestValidator()
        {
            RuleFor(x => x.NewRoom).Must(CustomValidationMethods.BeNullableSupported)
                .WithMessage($"Пожалуйста, выберите одно из допустимых значений: {string.Join(", ", Values.ValidRooms)}");
        }
    }
}
