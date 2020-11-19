using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class DeleteFriendshipAction
    {
        public ClientFriendship FriendshipToDelete { get; private set; }

        public DeleteFriendshipAction(ClientFriendship friendshipToDelete)
        {
            FriendshipToDelete = friendshipToDelete;
        }
    }

    public class DeleteFriendshipSuccessAction
    {
        public ClientFriendship DeletedFriendship { get; private set; }
        public DeleteFriendshipSuccessAction(ClientFriendship friendship)
        {
            DeletedFriendship = friendship;
        }
    }
}
