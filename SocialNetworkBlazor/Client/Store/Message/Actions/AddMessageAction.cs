using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Messages.Actions
{
    public class AddMessageAction
    {
        public ClientMessageCreate NewMessage { get; private set; }

        public AddMessageAction(ClientMessageCreate newMessage)
        {
            NewMessage = newMessage;
        }
    }

    public class AddMessageSuccessAction
    {
    }
}
