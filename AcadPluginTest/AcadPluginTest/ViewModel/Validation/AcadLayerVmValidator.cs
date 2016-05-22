using AcadPluginTest.ViewModel.Entities.Implementations;
using FluentValidation;

namespace AcadPluginTest.ViewModel.Validation
{
    /// <summary>
    /// Класс для валидации VM слоя
    /// </summary>
    public class AcadLayerVmValidator : AbstractValidator<AcadLayerVm>
    {
        /// <summary>
        /// Список символов запрещённых в имени слоя
        /// </summary>
        private readonly char[] _prohibitedSymbols = {'<', '>', '/', '\\', '"', '"', ':', ';', '?', '*', '|', ',', '=', '`'};

        public AcadLayerVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя слоя не может быть пустым")
                .Must(IsNotContinsProhibitedSymbols)
                .WithMessage("В имени слоя запрещены символы " + string.Join(" ", _prohibitedSymbols))
                .Length(1, 255)
                .WithMessage("Максимальная длина названгия слоя 255 символов");

        }

        /// <summary>
        /// Проверка на наличие запрещённых символов
        /// </summary>
        /// <param name="checkingString"></param>
        /// <returns></returns>
        private bool IsNotContinsProhibitedSymbols(string checkingString)
        {
            return  checkingString.IndexOfAny(_prohibitedSymbols) == -1;
        }
    }
}
