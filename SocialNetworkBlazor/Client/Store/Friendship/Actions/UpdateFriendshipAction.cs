using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class UpdateFriendshipAction
    {
        public ClientFriendshipUpdate UpdatedFriendship { get; private set; }
        public UpdateFriendshipAction(ClientFriendshipUpdate updatedFriendship)
        {
            UpdatedFriendship = updatedFriendship;
        }
    }

    public class UpdateFriendshipSuccessAction
    {
        public ClientFriendship Friendship { get; private set; }
        public UpdateFriendshipSuccessAction(ClientFriendship friendship)
        {
            Friendship = friendship;
        }
    }
}
