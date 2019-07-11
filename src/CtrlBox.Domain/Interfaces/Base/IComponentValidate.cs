using FluentValidation;
using FluentValidation.Results;

namespace CtrlBox.Domain.Interfaces.Base
{
    public interface IComponentValidate
    {
        bool IsValid { get; set; }
        ValidationResult ValidationResult { get; set; }

        bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
    }
}
