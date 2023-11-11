//using AutoMapper;
//using MediatR;
//using Oms.Base.Response;
//using Oms.Data.Context;
//using Oms.Data.Domain;
//using Oms.Schema;
//using Microsoft.EntityFrameworkCore;
//using static Oms.Operation.Cqrs.OrderItemCqrs;

//namespace Oms.Operation.Command;

//public class OrderItemCommandHandler :
//    IRequestHandler<CreateOrderItemCommand, ApiResponse<OrderItemResponse>>,
//    IRequestHandler<UpdateOrderItemCommand, ApiResponse>,
//    IRequestHandler<DeleteOrderItemCommand, ApiResponse>
//{
//    private readonly OmsDbContext dbContext;
//    private readonly IMapper mapper;

//    public OrderItemCommandHandler(OmsDbContext dbContext, IMapper mapper)
//    {
//        this.dbContext = dbContext;
//        this.mapper = mapper;
//    }

//    public async Task<ApiResponse<OrderItemResponse>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
//    {
//        OrderItem mapped = mapper.Map<OrderItem>(request.Model);

//        var entity = await dbContext.Set<OrderItem>().AddAsync(mapped, cancellationToken);
//        await dbContext.SaveChangesAsync(cancellationToken);

//        var response = mapper.Map<OrderItemResponse>(entity.Entity);
//        return new ApiResponse<OrderItemResponse>(response);
//    }

//    public async Task<ApiResponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await dbContext.Set<OrderItem>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
//        if (entity == null)
//        {
//            return new ApiResponse("Record not found!");
//        }
//        entity.Quantity = request.Model.Quantity;

//        await dbContext.SaveChangesAsync(cancellationToken);
//        return new ApiResponse();
//    }

//    public async Task<ApiResponse> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
//    {
//        var entity = await dbContext.Set<OrderItem>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
//        if (entity == null)
//        {
//            return new ApiResponse("Record not found!");
//        }

//        entity.IsActive = false;
//        await dbContext.SaveChangesAsync(cancellationToken);
//        return new ApiResponse();
//    }
//}