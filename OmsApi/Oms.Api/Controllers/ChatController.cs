using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oms.Base.Response;
using Oms.Schema;
using static Oms.Operation.Cqrs.ChatCqrs;

namespace Oms.Api.Controllers;

[Route("oms/api/v1/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private IMediator mediator;

    public ChatController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{receiverEmail}")]
    [Authorize(Roles = "admin,dealer")]
    public async Task<ApiResponse<List<ChatResponse>>> Get(string receiverEmail)
    {
        var mailClaim = User.Claims.FirstOrDefault(c => c.Type == "Email");
        string senderEmail = mailClaim.Value;

        var operation = new GetChatBySpecificEmailQuery(senderEmail, receiverEmail);
        var result = await mediator.Send(operation);
        return result;
    }

    
    [HttpGet()]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ChatResponse>>> GetAll()
    {
        var operation = new GetAllChatQuery();
        var result = await mediator.Send(operation);
        return result;
    }


    [HttpPost()]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<ChatResponse>> Post([FromBody] ChatRequest request)
    {
        var mailClaim = User.Claims.FirstOrDefault(c => c.Type == "Email");
        string senderEmail = mailClaim.Value;

        var operation = new CreateChatCommand(request, senderEmail);
        var result = await mediator.Send(operation);
        return result;
    }
}