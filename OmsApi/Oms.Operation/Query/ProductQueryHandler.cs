using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using static Oms.Operation.Cqrs.ProductCqrs;

namespace Oms.Operation.Query;

public class ProductQueryHandler :
    IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public ProductQueryHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        //
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserNumber == request.userNumber, cancellationToken);
        int profitMargin = ((int)entity.ProfitMargin);

        List<Product> list = await dbContext.Set<Product>()
            .ToListAsync(cancellationToken);

        foreach(var product in list)
        {
            product.BasePrice = product.BasePrice + (product.BasePrice * profitMargin / 100);
        }

        List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
        return new ApiResponse<List<ProductResponse>>(mapped);
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        Product? entity = await dbContext.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ProductResponse>("Record not found!");
        }

        var activeUser = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserNumber == request.userNumber, cancellationToken);
        int profitMargin = ((int)activeUser.ProfitMargin);
        entity.BasePrice = entity.BasePrice + (entity.BasePrice * profitMargin / 100);

        ProductResponse mapped = mapper.Map<ProductResponse>(entity);
        return new ApiResponse<ProductResponse>(mapped);
    }
}