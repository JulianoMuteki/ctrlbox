using CtrlBox.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Domain.Validations
{
    public class BoxValidator : AbstractValidator<Box>
    {
        public BoxValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Barcode).NotNull().Length(0, 14);
            RuleFor(x => x.Description).NotNull().Length(0, 250);
        }
    }
}
