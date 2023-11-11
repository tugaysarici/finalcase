using FluentValidation;
using Oms.Schema;

namespace Oms.Operation.Validation;

public class CreateProductValidator : AbstractValidator<ProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.BasePrice).GreaterThan(0).WithMessage("BasePrice should be a positive value.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(1).WithMessage("Stock is at least 1.");
        RuleFor(x => x.Description).NotEmpty().MinimumLength(4).WithMessage("Description is required and its minimum length is 4.");
        RuleFor(x => x.MinimumQuantity).GreaterThanOrEqualTo(0).LessThanOrEqualTo(x => x.Stock).WithMessage("MinimumQuantity is at least 0 and less than or equal to stock.");
    }
}