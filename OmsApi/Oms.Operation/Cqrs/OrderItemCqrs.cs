//using Oms.Base.Response;
//using MediatR;
//using Oms.Schema;

//namespace Oms.Operation.Cqrs;

//public class OrderItemCqrs
//{
//    public record CreateOrderItemCommand(OrderItemRequest Model) : IRequest<ApiResponse<OrderItemResponse>>;
//    public record UpdateOrderItemCommand(OrderItemRequest Model, int Id) : IRequest<ApiResponse>;
//    public record DeleteOrderItemCommand(int Id) : IRequest<ApiResponse>;
//    public record GetAllOrderItemQuery() : IRequest<ApiResponse<List<OrderItemResponse>>>;
//    public record GetOrderItemByIdQuery(int Id) : IRequest<ApiResponse<OrderItemResponse>>;
//}