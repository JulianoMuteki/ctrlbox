﻿using CtrlBox.Domain.Interfaces.Base;
using FluentValidation;
using FluentValidation.Results;

namespace CtrlBox.Domain.Common
{
    public class BaseValidate: IComponentValidate
    {
        public bool IsValid { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            this.IsValid = ValidationResult.IsValid;
            return this.IsValid;
        }
    }
}
