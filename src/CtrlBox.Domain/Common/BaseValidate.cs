using FluentValidation;
using FluentValidation.Results;

namespace CtrlBox.Domain.Common
{
    public abstract class BaseValidate
    {
        public bool IsValid { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            this.IsValid = ValidationResult.IsValid;
            return this.IsValid;
        }
    }
}
