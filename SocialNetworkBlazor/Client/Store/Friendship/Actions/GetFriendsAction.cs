using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
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
        public List<ClientFriendship> Friends { get; private set; }

        public GetFriendsSuccessAction(List<ClientFriendship> friends)
        {
            Friends = friends;
        }
    }
}
