using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class SendFriendshipAction
    {
        public ClientFriendshipCreate NewFriendship { get; private set; }

        public SendFriendshipAction(ClientFriendshipCreate newFriendship)
        {
            NewFriendship = newFriendship;
        }
    }

    public class SendFriendshipSuccessAction
    {
    }
}
