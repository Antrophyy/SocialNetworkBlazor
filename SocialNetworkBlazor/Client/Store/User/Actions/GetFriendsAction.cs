using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class GetFriendsAction
    {
        public string UserId { get; private set; }
        public GetFriendsAction(string userId)
        {
            UserId = userId;
        }
    }

    public class GetFriendsSuccessAction
    {
        public List<ClientUser> Friends { get; private set; }

        public GetFriendsSuccessAction(List<ClientUser> friends)
        {
            Friends = friends;
        }
    }
}
