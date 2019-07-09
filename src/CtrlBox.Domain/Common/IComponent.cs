using FluentValidation;
using FluentValidation.Results;

namespace CtrlBox.Domain.Common
{
    public interface IComponent
    {
        bool IsValid { get; set; }
        ValidationResult ValidationResult { get; set; }

        bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
    }
}
