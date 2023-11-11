//using FluentValidation;
//using Oms.Schema;

//namespace Oms.Operation.Validation;

//public class CreateOrderValidator : AbstractValidator<OrderRequest>
//{
//    public CreateOrderValidator()
//    {
//        RuleFor(x => x.PaymentMethod).NotEmpty().Must(paymentMethod => paymentMethod == "ElectronicFundsTransfer" || paymentMethod == "WireTransfer"
//            || paymentMethod == "CreditCard" || paymentMethod == "OpenAccount")
//            .WithMessage("Payment method must be one of the following: ElectronicFundsTransfer, WireTransfer, CreditCard, OpenAccount");

//        //RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Order must contain at least one order item.");
//        //RuleFor(x => x.UserId).NotEmpty();
//    }
//}