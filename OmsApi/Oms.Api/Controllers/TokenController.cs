using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oms.Base.Response;
using Oms.Operation;
using Oms.Schema;
using Oms.Api.Middleware;

namespace Oms.Api.Controllers;

[Route("oms/api/v1/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogActionFilter))]
    [TypeFilter(typeof(LogAuthorizationFilter))]
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogExceptionFilter))]
    [HttpGet("Test")]
    public ApiResponse Get()
    {
        return new ApiResponse();
    }

    [HttpGet("/tokenTest")]
    [Authorize]
    public bool TokenTest()
    {
        return true;
    }
}