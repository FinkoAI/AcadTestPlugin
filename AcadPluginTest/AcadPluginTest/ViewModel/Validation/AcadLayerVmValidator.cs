using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPluginTest.ViewModel.Entities.Implementations;
using Autodesk.AutoCAD.DatabaseServices;
using FluentValidation;

namespace AcadPluginTest.ViewModel.Validation
{
    public class AcadLayerVmValidator : AbstractValidator<AcadLayerVm>
    {
        private readonly char[] _prohibitedSymbols = new[] {'<', '>', '/', '\\', '"', '"', ':', ';', '?', '*', '|', ',', '=', '`'};

        public AcadLayerVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя слоя не может быть пустым")
                .Must(IsNotContinsProhibitedSymbols)
                .WithMessage("В имени слоя запрещены символы " + string.Join(" ", _prohibitedSymbols));
        }

        private bool IsNotContinsProhibitedSymbols(string checkingString)
        {
            return  checkingString.IndexOfAny(_prohibitedSymbols) == -1;
        }
    }
}
