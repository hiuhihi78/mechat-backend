using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Abstractions.Data.Dapper;
using MeChat.Domain.Abstractions.Services.External;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;
using Newtonsoft.Json;
using static MeChat.Domain.Shared.Exceptions.AuthExceptions;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class GetRefreshTokenQueryHandler : IQueryHandler<Query.RefreshToken, Response.Authenticated>
{
    private readonly ICacheService cacheService;
    private readonly IJwtService jwtTokenService;
    private readonly IUnitOfWork unitOfWork;
    private readonly AuthUtil authUtil;

    public GetRefreshTokenQueryHandler(
        ICacheService cacheService, 
        IJwtService jwtTokenService, 
        IUnitOfWork unitOfWork, 
        AuthUtil authUtil)
    {
        this.cacheService = cacheService;
        this.jwtTokenService = jwtTokenService;
        this.unitOfWork = unitOfWork;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.RefreshToken request, CancellationToken cancellationToken)
    {
        //check request is valid
        if (request.UserId == null || request.AccessToken == null)
            throw new AccessTokenInValid();

        //get user Id in acces token
        var rawUserId = jwtTokenService.GetClaim(AppConstants.Configuration.Jwt.id, request.AccessToken);
        if (rawUserId == null) throw new AccessTokenInValid();

        Guid userId = Guid.Parse(rawUserId.ToString()!);

        //check userId in request header with userId in accessToken is match
        if (!string.Equals(userId.ToString(), request.UserId, StringComparison.InvariantCultureIgnoreCase))
            throw new AccessTokenInValid();

        //get refresh token in access token
        var rawRefreshToken = jwtTokenService.GetClaim(AppConstants.Configuration.Jwt.jti, request.AccessToken);
        if (rawRefreshToken == null) throw new AccessTokenInValid();
        var refreshTokenInAccessToken = (string)rawRefreshToken;

        //check refresh token in request match with refresh token in access token
        if(request.Refresh != refreshTokenInAccessToken)
            throw new AccessTokenInValid();

        //Check user's permitssion
        var user = await unitOfWork.Users.FindByIdAsync(userId) ?? throw new UserNotHavePermission();

        if (user.Status != AppConstants.User.Status.Activate)
            return Result.Initialization<Response.Authenticated>(AppConstants.ResponseCodes.User.Banned, "User has been banned!", false, null);

        //Check refesh token
        var rawUserIdFromCacheWithRefreshToken = await cacheService.GetCache(request.Refresh!) ?? string.Empty;
        if(string.IsNullOrEmpty(rawUserIdFromCacheWithRefreshToken))
            return Result.Failure<Response.Authenticated>("Refresh token has been expried!");

        var userIdFromCacheWithRefreshToken = JsonConvert.DeserializeObject<string>(rawUserIdFromCacheWithRefreshToken);
        if (userIdFromCacheWithRefreshToken != user.Id.ToString())
            return Result.Failure<Response.Authenticated>("Invalid refresh token!");

        //Remove old refresh token from cache
        await cacheService.RemoveCache(request.Refresh!);

        return await authUtil.GenerateToken(user);
    }

}
