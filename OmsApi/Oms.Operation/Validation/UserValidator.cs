using FluentValidation;
using Oms.Schema;

namespace Oms.Operation.Validation;

public class CreateUserValidator : AbstractValidator<UserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Email).MinimumLength(10).WithMessage("Email length min value is 10.");

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required.");
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Firstname length min value is 3.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
        RuleFor(x => x.LastName).MinimumLength(2).WithMessage("LastName length min value is 2.");

        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.Address).MinimumLength(10).WithMessage("Address length min value is 10.");

        //RuleFor(x => x.Role).NotEmpty().Must(role => role == "dealer").WithMessage("Role must be either 'dealer' or 'admin'.");
        //RuleFor(x => x.Role).NotEmpty().Must(role => role == "dealer" || role == "admin").WithMessage("Role must be either 'dealer' or 'admin'.");
    }
}