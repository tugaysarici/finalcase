using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oms.Base.Response;
using Oms.Schema;
using static Oms.Operation.Cqrs.ProductCqrs;

namespace Oms.Api.Controllers;

[Route("oms/api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin,dealer")]
    public async Task<ApiResponse<List<ProductResponse>>> GetAll()
    {
        var userNumberClaim = User.Claims.FirstOrDefault(c => c.Type == "UserNumber");
        int userNumber = int.Parse(userNumberClaim.Value);
        /*****
        string email;
        string role;
        var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "Email");
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "Role");
        if (userNumberClaim != null)
        {
            userNumber = int.Parse(userNumberClaim.Value);
            email = emailClaim.Value;
            role = roleClaim.Value;
        }*****/

        var operation = new GetAllProductQuery(userNumber);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin,dealer")]
    public async Task<ApiResponse<ProductResponse>> Get(int id)
    {
        var operation = new GetProductByIdQuery(id, int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserNumber").Value));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest request)
    {
        var operation = new CreateProductCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] ProductRequest request)
    {
        var operation = new UpdateProductCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteProductCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}