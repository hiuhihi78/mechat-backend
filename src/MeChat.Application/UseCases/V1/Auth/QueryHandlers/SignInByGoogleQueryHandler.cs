using Google.Apis.Auth;
using MeChat.Application.UseCases.V1.Auth.Utils;
using MeChat.Domain.Abstractions.Data.EntityFramework.Repositories;
using MeChat.Domain.Abstractions.Messages.DomainEvents.Base;
using MeChat.Domain.Entities;
using MeChat.Domain.Shared.Constants;
using MeChat.Domain.Shared.Exceptions.Base;
using MeChat.Domain.Shared.Responses;
using MeChat.Domain.UseCases.V1.Auth;
using Microsoft.Extensions.Configuration;

namespace MeChat.Application.UseCases.V1.Auth.QueryHandlers;
public class SignInByGoogleQueryHandler : IQueryHandler<Query.SignInByGoogle, Response.Authenticated>
{
    private readonly IConfiguration configuration;
    private readonly Domain.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper;
    private readonly Domain.Abstractions.Data.EntityFramework.IUnitOfWork unitOfWorkEF;
    private readonly IRepositoryBase<Domain.Entities.User, Guid> userRepository;
    private readonly IRepository<UserSocial> userSocialRepository;
    private readonly AuthUtil authUtil;

    public SignInByGoogleQueryHandler(
        IConfiguration configuration,
        Domain.Abstractions.Data.Dapper.IUnitOfWork unitOfWorkDapper,
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
        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken);
        }
        catch
        {
            throw new UnAuthenticationException("Invalid google token!");
        }

        if (payload is null)
            throw new UnAuthenticationException("Invalid google token!");

        var existed = await unitOfWorkDapper.Users.GetUserByAccountSocial(
            payload.Subject, AppConstants.Social.Google);

        if (existed is not null)
        {
            existed.EnsureCanSignIn();
            return await authUtil.GenerateToken(existed);
        }

        var newUser = Domain.Entities.User.CreateFromSocialLogin(
            id: Guid.NewGuid(),
            email: payload.Email,
            fullname: payload.Name,
            avatar: payload.Picture,
            defaultRoleId: AppConstants.Role.User);

        var userSocial = UserSocial.Create(
            userId: newUser.Id,
            socialId: AppConstants.Social.Google,
            accountSocialId: payload.Subject);

        userRepository.Add(newUser);
        userSocialRepository.Add(userSocial);

        await unitOfWorkEF.SaveChangeAsync();

        return await authUtil.GenerateToken(newUser);
    }
}
