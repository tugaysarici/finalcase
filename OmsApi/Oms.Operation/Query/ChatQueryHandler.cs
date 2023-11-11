using AutoMapper;
using MediatR;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using Microsoft.EntityFrameworkCore;
using static Oms.Operation.Cqrs.ChatCqrs;

namespace Oms.Operation.Query;

public class ChatQueryHandler :
    IRequestHandler<GetAllChatQuery, ApiResponse<List<ChatResponse>>>,
    IRequestHandler<GetChatBySpecificEmailQuery, ApiResponse<List<ChatResponse>>>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public ChatQueryHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ChatResponse>>> Handle(GetAllChatQuery request, CancellationToken cancellationToken)
    {
        List<Chat> list = await dbContext.Set<Chat>()
            .ToListAsync(cancellationToken);

        List<ChatResponse> mapped = mapper.Map<List<ChatResponse>>(list);
        return new ApiResponse<List<ChatResponse>>(mapped);
    }

    public async Task<ApiResponse<List<ChatResponse>>> Handle(GetChatBySpecificEmailQuery request,
        CancellationToken cancellationToken)
    {
        List<Chat> list = await dbContext.Set<Chat>()
            .Where(x => (x.SenderEmail == request.senderEmail && x.ReceiverEmail == request.receiverEmail) 
                     || (x.SenderEmail == request.receiverEmail && x.ReceiverEmail == request.senderEmail))
            .ToListAsync(cancellationToken);

        List<ChatResponse> mapped = mapper.Map<List<ChatResponse>>(list);
        return new ApiResponse<List<ChatResponse>>(mapped);
    }
}