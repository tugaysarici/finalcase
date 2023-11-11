using Oms.Base.Response;
using MediatR;
using Oms.Schema;

namespace Oms.Operation.Cqrs;

public class ProductCqrs
{
    public record CreateProductCommand(ProductRequest Model) : IRequest<ApiResponse<ProductResponse>>;
    public record UpdateProductCommand(ProductRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteProductCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllProductQuery(int userNumber) : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int Id, int userNumber) : IRequest<ApiResponse<ProductResponse>>;
}