using Google.Apis.Auth;
using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Common.Shared.Constants;
using MeChat.Domain.Abstractions.Data.Dapper;
using MeChat.Domain.Abstractions.Data.EntityFramework;
using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.MessageBroker.Messages.DomainEvents;
using MeChat.Domain.Entities;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInByGoogleQueryHandler : IQueryHandler<Query.SignInByGoogle, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly IUnitOfWork unitOfWorkDapper;
    private readonly Domain.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<UserSocial> userSocialRepository;
    private readonly AuthUtil authUtil;

    public SignInByGoogleQueryHandler(
        IConfiguration configuration,
        IUnitOfWork unitOfWorkDapper,
        Domain.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF, 
        IRepositoryBase<Domain.Entities.User, Guid> userRepository, 
        IRepository<UserSocial> userSocialRepository,
        AuthUtil authUtil)
    {
        this.configuration = configuration;
        this.unitOfWorkDapper = unitOfWorkDapper;
        this.unitOfWorkEF = unitOfWorkEF;
        this.userRepository = userRepository;
        this.userSocialRepository = userSocialRepository;
        this.authUtil = authUtil;
    }

    public async Task<Result<Response.Authenticated>> Handle(Query.SignInByGoogle request, CancellationToken cancellationToken)
    {
        //Check Google token
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken);
        if(payload == null) 
            return Result.UnAuthentication<Response.Authenticated>("Invalid google token!");

        //Check user's email existed
        var user = await unitOfWorkDapper.Users.GetUserByAccountSocial(payload.Subject, AppConstants.Social.Google);
        if(user != null)
        {
            return await authUtil.GenerateToken(user);
        }

        //New User
        var newUser = Domain.Entities.User.CreateForTest(
            email: payload.Email,
            fullname: payload.Name,
            avatar: payload.Picture,
            defaultRoleId: AppConstants.Role.User
        );

        userRepository.Add(newUser);
        await unitOfWorkEF.SaveChangeAsync();

        //New UserSocial
        UserSocial userSocial = new UserSocial
        {
            UserId = newUser.Id,
            SocialId = AppConstants.Social.Google,
            AccountSocialId = payload.Subject,
        };
        userSocialRepository.Add(userSocial);
        await unitOfWorkEF.SaveChangeAsync();

        return await authUtil.GenerateToken(newUser);
    }
}
