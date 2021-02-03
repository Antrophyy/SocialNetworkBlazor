using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class DeleteFriendshipAction
    {
        public string User1Id { get; private set; }
        public string User2Id { get; private set; }

        public DeleteFriendshipAction(string user1Id, string user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }
    }

    public class DeleteFriendshipSuccessAction
    {
        public string User1Id { get; private set; }
        public string User2Id { get; private set; }
        public DeleteFriendshipSuccessAction(string user1Id, string user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }
    }
}
