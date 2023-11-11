using MediatR;
using Oms.Base.Response;
using Oms.Schema;

namespace Oms.Operation.Cqrs;

public class ChatCqrs
{
    public record CreateChatCommand(ChatRequest Model, string senderEmail) : IRequest<ApiResponse<ChatResponse>>;
    public record GetAllChatQuery() : IRequest<ApiResponse<List<ChatResponse>>>;
    public record GetChatBySpecificEmailQuery(string senderEmail, string receiverEmail) : IRequest<ApiResponse<List<ChatResponse>>>;
}