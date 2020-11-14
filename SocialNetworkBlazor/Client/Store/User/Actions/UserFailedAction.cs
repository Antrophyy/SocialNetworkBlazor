namespace SocialNetworkBlazor.Client.Store.Users.Actions
{
    public class UserFailedAction
    {
        public string ErrorTitle { get; private set; }
        public string ErrorMessage { get; private set; }

        public UserFailedAction(string errorTitle, string errorMessage)
        {
            ErrorTitle = errorTitle;
            ErrorMessage = errorMessage;
        }
    }
}
