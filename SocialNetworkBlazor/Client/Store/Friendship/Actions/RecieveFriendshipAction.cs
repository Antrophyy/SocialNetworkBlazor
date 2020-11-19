using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class RecieveFriendshipAction
    {
        public ClientFriendship Friendship { get; private set; }

        public RecieveFriendshipAction(ClientFriendship friendship)
        {
            Friendship = friendship;
        }
    }
}
