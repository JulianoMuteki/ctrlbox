using CtrlBox.Domain.Entities;
using FluentValidation;

namespace CtrlBox.Domain.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(0, 200);
            RuleFor(x => x.UnitMeasure).NotNull().Length(0, 150);
            RuleFor(x => x.Description).NotNull().Length(0, 250);
        }
    }
}
