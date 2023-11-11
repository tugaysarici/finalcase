//using AutoMapper;
//using MediatR;
//using Oms.Base.Response;
//using Oms.Data.Context;
//using Oms.Data.Domain;
//using Oms.Schema;
//using Microsoft.EntityFrameworkCore;
//using static Oms.Operation.Cqrs.OrderItemCqrs;

//namespace Oms.Operation.Query;

//public class OrderItemQueryHandler :
//    IRequestHandler<GetAllOrderItemQuery, ApiResponse<List<OrderItemResponse>>>,
//    IRequestHandler<GetOrderItemByIdQuery, ApiResponse<OrderItemResponse>>
//{
//    private readonly OmsDbContext dbContext;
//    private readonly IMapper mapper;

//    public OrderItemQueryHandler(OmsDbContext dbContext, IMapper mapper)
//    {
//        this.dbContext = dbContext;
//        this.mapper = mapper;
//    }

//    public async Task<ApiResponse<List<OrderItemResponse>>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
//    {
//        List<OrderItem> list = await dbContext.Set<OrderItem>()
//            .ToListAsync(cancellationToken);

//        List<OrderItemResponse> mapped = mapper.Map<List<OrderItemResponse>>(list);
//        return new ApiResponse<List<OrderItemResponse>>(mapped);
//    }

//    public async Task<ApiResponse<OrderItemResponse>> Handle(GetOrderItemByIdQuery request,
//        CancellationToken cancellationToken)
//    {
//        OrderItem? entity = await dbContext.Set<OrderItem>()
//            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

//        if (entity == null)
//        {
//            return new ApiResponse<OrderItemResponse>("Record not found!");
//        }

//        OrderItemResponse mapped = mapper.Map<OrderItemResponse>(entity);
//        return new ApiResponse<OrderItemResponse>(mapped);
//    }
//}