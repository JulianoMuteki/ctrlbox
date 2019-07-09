using CtrlBox.Domain.Entities;
using FluentValidation;

namespace CtrlBox.Domain.Validations
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(0, 255);
            RuleFor(x => x.Phone).NotNull().Length(0, 255);
        }
    }
}
