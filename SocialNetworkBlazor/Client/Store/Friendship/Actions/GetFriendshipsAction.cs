using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class GetFriendshipsAction
    {
        public string UserId { get; private set; }
        public GetFriendshipsAction(string userId)
        {
            UserId = userId;
        }
    }

    public class GetFriendshipsSuccessAction
    {
        public List<ClientFriendship> ClientFriendships { get; private set; }
        public GetFriendshipsSuccessAction(List<ClientFriendship> clientFriendships)
        {
            ClientFriendships = clientFriendships;
        }
    }
}
