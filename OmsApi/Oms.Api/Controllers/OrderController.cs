using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oms.Base.Response;
using Oms.Schema;
using static Oms.Operation.Cqrs.OrderCqrs;

namespace Oms.Api.Controllers;

[Route("oms/api/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    //[HttpGet]
    //[Authorize(Roles = "admin")]
    //public async Task<ApiResponse<List<OrderResponse>>> GetAll()
    //{
    //    var operation = new GetAllOrderQuery();
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    //[HttpGet("{id}")]
    //[Authorize(Roles = "admin")]
    //public async Task<ApiResponse<OrderResponse>> Get(int id)
    //{
    //    var operation = new GetOrderByIdQuery(id);
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    //[HttpGet("GetOwnOrder")]
    //[Authorize(Roles = "dealer")]
    //public async Task<ApiResponse<List<OrderResponse>>> GetOwnOrder()
    //{
    //    var operation = new GetOwnOrderQuery();
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest request)
    {
        var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
        int userId = int.Parse(idClaim.Value);

        var operation = new CreateOrderCommand(request, userId);
        var result = await mediator.Send(operation);
        return result;
    }

    //[HttpPut("{id}/{status}")]
    //[Authorize(Roles = "admin")]
    //public async Task<ApiResponse> Put(int id, bool orderStatus, [FromBody] OrderRequest request)
    //{
    //    var operation = new UpdateOrderCommand(request, id, orderStatus);
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    //[HttpDelete("{id}")]
    //[Authorize(Roles = "admin")]
    //public async Task<ApiResponse> Delete(int id)
    //{
    //    var operation = new DeleteOrderCommand(id);
    //    var result = await mediator.Send(operation);
    //    return result;
    //}
}