namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class FriendshipUpdateUserOnlineStatusAction
    {
        public bool IsOnline { get; private set; }
        public int ContactId { get; private set; }

        public FriendshipUpdateUserOnlineStatusAction(bool isOnline, int contactId)
        {
            IsOnline = isOnline;
            ContactId = contactId;
        }
    }
}
