using AutoMapper;
using MediatR;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using Microsoft.EntityFrameworkCore;
using static Oms.Operation.Cqrs.OrderCqrs;
using System.Data.Entity;

namespace Oms.Operation.Command;

public class OrderCommandHandler :
    IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>
    //,IRequestHandler<UpdateOrderCommand, ApiResponse>
    //,IRequestHandler<DeleteOrderCommand, ApiResponse>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public OrderCommandHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order mapped = mapper.Map<Order>(request.Model);
        mapped.User = dbContext.Set<User>().FirstOrDefault(x => x.Id == request.userId);
        mapped.UserId = request.userId;
        mapped.OrderNumber = dbContext.Set<Order>().Select(x => (int?)x.OrderNumber).Max() ?? 0 + 1;
        mapped.OrderItems.ForEach(x => x.Product = dbContext.Set<Product>().FirstOrDefault(y => y.Id == x.ProductId));
        decimal amount = 0;
        foreach(var item in mapped.OrderItems)
        {
            if(item.Product.Stock < item.Quantity)
            {
                return new ApiResponse<OrderResponse>("Insufficient stock.");
            }
            amount += (item.Product.BasePrice + (item.Product.BasePrice * mapped.User.ProfitMargin / 100)) * item.Quantity;
            item.Product.Stock -= item.Quantity;
        }
        mapped.OrderAmount = amount;
        mapped.Id = dbContext.Set<Order>().Select(x => (int?)x.Id).Max() ?? 0 + 1;

        var entity = await dbContext.Set<Order>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<OrderResponse>(entity.Entity);
        return new ApiResponse<OrderResponse>(response);
    }

    //public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    //{
    //    var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    //    if (entity == null)
    //    {
    //        return new ApiResponse("Record not found!");
    //    }
    //    entity.OrderStatus = request.orderStatus;

    //    await dbContext.SaveChangesAsync(cancellationToken);
    //    return new ApiResponse();
    //}

    //public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    //{
    //    var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    //    if (entity == null)
    //    {
    //        return new ApiResponse("Record not found!");
    //    }

    //    entity.IsActive = false;
    //    await dbContext.SaveChangesAsync(cancellationToken);
    //    return new ApiResponse();
    //}
}