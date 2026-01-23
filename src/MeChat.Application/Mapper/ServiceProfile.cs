using AutoMapper;
using MeChat.Domain.Entities;
using MeChat.Domain.Shared.Responses;

namespace MeChat.Application.Mapper;
public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        #region V1

        #region User
        CreateMap<User, Domain.UseCases.V1.User.Command.AddUser>().ReverseMap();
        CreateMap<User, Domain.UseCases.V1.User.Command.UpdateUser>().ReverseMap();
        CreateMap<User, Domain.UseCases.V1.User.Response.User>().ReverseMap();
        CreateMap<PageResult<User>, PageResult<Domain.UseCases.V1.User.Response.User>>().ReverseMap();
        CreateMap<User, Domain.UseCases.V1.User.Response.UserPublicInfo>()
            .ForMember(des => des.Friends, atc => atc.MapFrom(src => new List<Domain.UseCases.V1.User.Response.UserPublicInfo>()))
            .ReverseMap();
        #endregion

        #region Auth
        CreateMap<User, Domain.UseCases.V1.Auth.Response.UserInfo>()
            .ForMember(des => des.UserId, atc => atc.MapFrom(src => src.Id))
            .ReverseMap();
        #endregion

        #region Notification
        CreateMap<Notification, Domain.UseCases.V1.Notification.Response.Notification>()
            .ForMember(des => des.RequesterId, opt => opt.MapFrom(src => src.Requester!.Id))
            .ForMember(des => des.RequesterName, atc => atc.MapFrom(src => src.Requester!.Fullname == null ? string.Empty : src.Requester!.Fullname))
            .ForMember(des => des.Image, atc => atc.MapFrom(src => src.Requester!.Avatar == null ? string.Empty : src.Requester!.Avatar))
            .ReverseMap();
        #endregion

        #endregion

        #region V2

        #endregion
    }
}
