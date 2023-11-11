using Oms.Base.Response;
using MediatR;
using Oms.Schema;

namespace Oms.Operation.Cqrs;

public class OrderCqrs
{
    public record CreateOrderCommand(OrderRequest Model, int userId) : IRequest<ApiResponse<OrderResponse>>;
    //public record UpdateOrderCommand(OrderRequest Model, int Id, bool orderStatus) : IRequest<ApiResponse>;
    //public record DeleteOrderCommand(int Id) : IRequest<ApiResponse>;
    //public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
    //public record GetOrderByIdQuery(int Id) : IRequest<ApiResponse<OrderResponse>>;
    //public record GetOwnOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
}