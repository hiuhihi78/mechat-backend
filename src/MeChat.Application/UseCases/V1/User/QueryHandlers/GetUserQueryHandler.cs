using AutoMapper;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Abstractions.Data.Dapper;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Exceptions;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.User;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;

namespace MeChat.Application.UseCases.V1.User.QueryHandlers;
public class GetUserQueryHandler : IQueryHandler<Query.GetUserById, Response.User>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IJwtService jwtService;

    public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.jwtService = jwtService;
    }

    public async Task<Result<Response.User>> Handle(Query.GetUserById request, CancellationToken cancellationToken)
    {
        var userIdInToken = jwtService.GetClaim(AppConstants.Configuration.Jwt.id, request.AccessToken, false)?.ToString();
        if (userIdInToken == null)
            return Result.UnAuthentication<Response.User>("Invalid user id!");

        if (request.Id.ToString().ToLower() != userIdInToken.ToLower())
            return Result.UnAuthentication<Response.User>("Invalid user id!");

        var user = await unitOfWork.Users.FindByIdAsync(request.Id) ?? throw new UserExceptions.NotFound(request.Id);
        if (user.Status != AppConstants.User.Status.Activate)
            return Result.UnAuthorized<Response.User>("Your account has been banned!");

        var result = mapper.Map<Response.User>(user);
        return Result.Success(result);
    }
}
