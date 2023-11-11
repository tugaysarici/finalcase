using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oms.Base.Response;
using Oms.Data.Context;
using Oms.Data.Domain;
using Oms.Schema;
using Oms.Base.Encryption;
using static Oms.Operation.Cqrs.UserCqrs;
using Oms.Operation.Validation;

namespace Oms.Operation.Command;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand, ApiResponse>,
    IRequestHandler<DeleteUserCommand, ApiResponse>
{
    private readonly OmsDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(OmsDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User mapped = mapper.Map<User>(request.Model);

        //
        if (mapped.Role == "admin")
        {
            return new ApiResponse<UserResponse>("There can only be one admin in the system.");
        }
        CreateUserValidator validator = new CreateUserValidator();
        var result = validator.Validate(request.Model);
        if(!result.IsValid)
        {
            return new ApiResponse<UserResponse>(result.Errors[0].ErrorMessage);
        }
        mapped.Password = Md5.Create(mapped.Password).ToLower();
        mapped.UserNumber = dbContext.Set<User>().Max(x => x.UserNumber) + 1;


        var entity = await dbContext.Set<User>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<UserResponse>(entity.Entity);
        return new ApiResponse<UserResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        // check
        entity.FirstName = request.Model.FirstName;
        entity.LastName = request.Model.LastName;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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