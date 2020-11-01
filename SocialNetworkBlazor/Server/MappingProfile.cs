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
        }
    }
}
