using CtrlBox.Domain.Entities;
using FluentValidation;

namespace CtrlBox.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull()
                    .Length(3, 10).WithMessage("Name 1 must be between 3 and 10 characters.");
            RuleFor(x => x.UnitMeasure).NotNull().Length(0, 150);
            RuleFor(x => x.Description).NotNull().Length(0, 250);
        }
    }
}
