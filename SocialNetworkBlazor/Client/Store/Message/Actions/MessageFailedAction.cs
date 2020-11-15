namespace SocialNetworkBlazor.Client.Store.Message.Actions
{
    public class MessageFailedAction
    {
        public string ErrorTitle { get; private set; }
        public string ErrorMessage { get; private set; }

        public MessageFailedAction(string errorTitle, string errorMessage)
        {
            ErrorTitle = errorTitle;
            ErrorMessage = errorMessage;
        }
    }
}
