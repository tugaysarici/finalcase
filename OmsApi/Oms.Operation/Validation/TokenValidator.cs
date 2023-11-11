using FluentValidation;
using Oms.Schema;

namespace Oms.Operation.Validation;

public class CreateTokenValidator : AbstractValidator<TokenRequest>
{

    public CreateTokenValidator()
    {
        RuleFor(x => x.UserNumber).NotEmpty().WithMessage("UserNumber is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithMessage("Password is required.");
    }
}