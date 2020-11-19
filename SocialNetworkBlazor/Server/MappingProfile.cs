using AutoMapper;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, ClientUser>();
            CreateMap<ClientUser, User>();

            CreateMap<ClientUserUpdate, User>();

            // Message
            CreateMap<Message, ClientMessage>();
            CreateMap<ClientMessage, Message>();

            CreateMap<ClientMessageCreate, Message>();
            CreateMap<Message, ClientMessageCreate>();

            // Post
            CreateMap<Post, ClientPost>();
            CreateMap<ClientPost, Post>();

            CreateMap<Post, ClientPostCreate>();
            CreateMap<ClientPostCreate, Post>();
            
            // Comment
            CreateMap<Comment, ClientComment>();
            CreateMap<ClientComment, Comment>();

            CreateMap<Comment, ClientCommentCreate>();
            CreateMap<ClientCommentCreate, Comment>();

            // Friendship
            CreateMap<Friendship, ClientFriendship>();
            CreateMap<ClientFriendship, Friendship>();

            CreateMap<ClientFriendshipCreate, Friendship>();
            CreateMap<Friendship, ClientFriendshipCreate>();

            CreateMap<ClientFriendshipUpdate, Friendship>();
            CreateMap<Friendship, ClientFriendshipUpdate>();

            CreateMap<ClientFriendshipUpdate, ClientFriendship>();
            CreateMap<ClientFriendship, ClientFriendshipUpdate>();

            CreateMap<ClientFriendshipCreate, ClientFriendship>();
            CreateMap<ClientFriendship, ClientFriendshipCreate>();
        }
    }
}
