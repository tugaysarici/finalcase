using AutoMapper;
using MediatR;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using Microsoft.EntityFrameworkCore;
using static Oms.Operation.Cqrs.UserCqrs;

namespace Oms.Operation.Query;

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>,
    IRequestHandler<GetOwnInfoQuery, ApiResponse<UserResponse>>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public UserQueryHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<User> list = await dbContext.Set<User>().Where(x => x.IsActive == true)
            .ToListAsync(cancellationToken);

        List<UserResponse> mapped = mapper.Map<List<UserResponse>>(list);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User? entity = await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == true, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<UserResponse>("Record not found!");
        }

        UserResponse mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetOwnInfoQuery request,
        CancellationToken cancellationToken)
    {
        User? entity = await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.UserNumber == request.userNumber, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<UserResponse>("Record not found!");
        }

        UserResponse mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }
}