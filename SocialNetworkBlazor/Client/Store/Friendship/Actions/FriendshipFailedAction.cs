namespace SocialNetworkBlazor.Client.Store.Friendship.Actions
{
    public class FriendshipFailedAction
    {
        public string ErrorTitle { get; private set; }
        public string ErrorMessage { get; private set; }

        public FriendshipFailedAction(string errorTitle, string errorMessage)
        {
            ErrorTitle = errorTitle;
            ErrorMessage = errorMessage;
        }
    }
}
