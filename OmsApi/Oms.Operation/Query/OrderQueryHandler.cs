//using AutoMapper;
//using MediatR;
//using Oms.Base.Response;
//using Oms.Data.Context;
//using Oms.Data.Domain;
//using Oms.Schema;
//using Microsoft.EntityFrameworkCore;
//using static Oms.Operation.Cqrs.OrderCqrs;

//namespace Oms.Operation.Query;

//public class OrderQueryHandler :
//    IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
//    IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>
//{
//    private readonly OmsDbContext dbContext;
//    private readonly IMapper mapper;

//    public OrderQueryHandler(OmsDbContext dbContext, IMapper mapper)
//    {
//        this.dbContext = dbContext;
//        this.mapper = mapper;
//    }

//    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
//    {
//        List<Order> list = await dbContext.Set<Order>()
//            .ToListAsync(cancellationToken);

//        List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);
//        return new ApiResponse<List<OrderResponse>>(mapped);
//    }

//    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request,
//        CancellationToken cancellationToken)
//    {
//        Order? entity = await dbContext.Set<Order>()
//            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

//        if (entity == null)
//        {
//            return new ApiResponse<OrderResponse>("Record not found!");
//        }

//        OrderResponse mapped = mapper.Map<OrderResponse>(entity);
//        return new ApiResponse<OrderResponse>(mapped);
//    }
//}