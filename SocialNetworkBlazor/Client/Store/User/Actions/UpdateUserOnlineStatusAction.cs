namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class UpdateUserOnlineStatusAction
    {
        public bool IsOnline { get; private set; }
        public int ContactId { get; private set; }

        public UpdateUserOnlineStatusAction(bool isOnline, int contactId)
        {
            IsOnline = isOnline;
            ContactId = contactId;
        }
    }
}
