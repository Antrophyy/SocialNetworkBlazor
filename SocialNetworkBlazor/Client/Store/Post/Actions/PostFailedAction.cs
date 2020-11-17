namespace SocialNetworkBlazor.Client.Store.Post.Actions
{
    public class PostFailedAction
    {
        public string ErrorTitle { get; private set; }
        public string ErrorMessage { get; private set; }

        public PostFailedAction(string errorTitle, string errorMessage)
        {
            ErrorTitle = errorTitle;
            ErrorMessage = errorMessage;
        }
    }
}
