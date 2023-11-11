//using FluentValidation;
//using Oms.Schema;

//namespace Oms.Operation.Validation;

//public class CreateOrderItemValidator : AbstractValidator<OrderItemRequest>
//{
//    public CreateOrderItemValidator()
//    {
//        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);
//        RuleFor(x => x.ProductId).NotEmpty();
//        RuleFor(x => x.OrderId).NotEmpty();
//    }
//}