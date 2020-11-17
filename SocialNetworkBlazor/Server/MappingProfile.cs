using AutoMapper;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ClientUser>();
            CreateMap<ClientUser, User>();

            CreateMap<ClientUserUpdate, User>();

            CreateMap<Message, ClientMessage>();
            CreateMap<ClientMessage, Message>();

            CreateMap<ClientMessageCreate, Message>();
            CreateMap<Message, ClientMessageCreate>();

            CreateMap<Post, ClientPost>();
            CreateMap<ClientPost, Post>();

            CreateMap<Post, ClientPostCreate>();
            CreateMap<ClientPostCreate, Post>();

            CreateMap<Comment, ClientComment>();
            CreateMap<ClientComment, Comment>();

            CreateMap<Comment, ClientCommentCreate>();
            CreateMap<ClientCommentCreate, Comment>();
        }
    }
}
