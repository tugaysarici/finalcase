//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Oms.Base.Response;
//using Oms.Schema;
//using static Oms.Operation.Cqrs.OrderItemCqrs;

//namespace Oms.Api.Controllers;

//[Route("oms/api/v1/[controller]")]
//[ApiController]
//public class OrderItemController : ControllerBase
//{
//    private IMediator mediator;

//    public OrderItemController(IMediator mediator)
//    {
//        this.mediator = mediator;
//    }

//    [HttpGet]
//    [Authorize(Roles = "admin")]
//    public async Task<ApiResponse<List<OrderItemResponse>>> GetAll()
//    {
//        var operation = new GetAllOrderItemQuery();
//        var result = await mediator.Send(operation);
//        return result;
//    }

//    [HttpGet("{id}")]
//    [Authorize(Roles = "admin")]
//    public async Task<ApiResponse<OrderItemResponse>> Get(int id)
//    {
//        var operation = new GetOrderItemByIdQuery(id);
//        var result = await mediator.Send(operation);
//        return result;
//    }

//    [HttpPost]
//    [Authorize(Roles = "admin")]
//    public async Task<ApiResponse<OrderItemResponse>> Post([FromBody] OrderItemRequest request)
//    {
//        var operation = new CreateOrderItemCommand(request);
//        var result = await mediator.Send(operation);
//        return result;
//    }

//    [HttpPut("{id}")]
//    [Authorize(Roles = "admin")]
//    public async Task<ApiResponse> Put(int id, [FromBody] OrderItemRequest request)
//    {
//        var operation = new UpdateOrderItemCommand(request, id);
//        var result = await mediator.Send(operation);
//        return result;
//    }

//    [HttpDelete("{id}")]
//    [Authorize(Roles = "admin")]
//    public async Task<ApiResponse> Delete(int id)
//    {
//        var operation = new DeleteOrderItemCommand(id);
//        var result = await mediator.Send(operation);
//        return result;
//    }
//}