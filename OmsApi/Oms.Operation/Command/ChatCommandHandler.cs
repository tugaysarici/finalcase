using AutoMapper;
using MediatR;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using System.Data.Entity;
using static Oms.Operation.Cqrs.ChatCqrs;

namespace Oms.Operation.Command;

public class ChatCommandHandler : IRequestHandler<CreateChatCommand, ApiResponse<ChatResponse>>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public ChatCommandHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ChatResponse>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if(!(request.senderEmail == "info@tsrc.com.tr" ^ request.Model.ReceiverEmail == "info@tsrc.com.tr"))
        {
            return new ApiResponse<ChatResponse>("Chat is only allowed between dealer and admin.");
        }

        Chat mapped = mapper.Map<Chat>(request.Model);

        mapped.SenderEmail = request.senderEmail;
        var entity = await dbContext.Set<Chat>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ChatResponse>(entity.Entity);
        return new ApiResponse<ChatResponse>(response);
    }
}