using CtrlBox.Domain.Entities;
using FluentValidation;
using System;

namespace CtrlBox.Domain.Validations
{
    public class BoxTypeValidator : AbstractValidator<BoxType>
    {
        public BoxTypeValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotNull().Length(0, 50);
            RuleFor(x => x.Description).NotNull().Length(0, 250);
        }
    }
}
