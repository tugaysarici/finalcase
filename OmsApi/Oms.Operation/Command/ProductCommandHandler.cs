using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using static Oms.Operation.Cqrs.ProductCqrs;

namespace Oms.Operation.Command;

public class ProductCommandHandler :
    IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>,
    IRequestHandler<UpdateProductCommand, ApiResponse>,
    IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public ProductCommandHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product mapped = mapper.Map<Product>(request.Model);

        var entity = await dbContext.Set<Product>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<ProductResponse>(entity.Entity);
        return new ApiResponse<ProductResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        // check
        entity.Name = request.Model.Name;
        entity.BasePrice = request.Model.BasePrice;
        entity.Stock = request.Model.Stock;
        entity.Description = request.Model.Description;
        entity.MinimumQuantity = request.Model.MinimumQuantity;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
    // ApiResponse descriptions can be specified.
}