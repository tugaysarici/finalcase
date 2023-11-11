using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oms.Base.Response;
using Oms.Schema;
using static Oms.Operation.Cqrs.UserCqrs;

namespace Oms.Api.Controllers;

[Route("oms/api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<UserResponse>>> GetAll()
    {
        var operation = new GetAllUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<UserResponse>> Get(int id)
    {
        var operation = new GetUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("OwnInfo")]
    [Authorize(Roles = "admin,dealer")]
    public async Task<ApiResponse<UserResponse>> GetOwnInfo()
    {
        var userNumberClaim = User.Claims.FirstOrDefault(c => c.Type == "UserNumber");

        var operation = new GetOwnInfoQuery(int.Parse(userNumberClaim.Value));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] UserRequest request)
    {
        var operation = new CreateUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UserRequest request)
    {
        var operation = new UpdateUserCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}