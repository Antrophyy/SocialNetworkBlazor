using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Message.Actions
{
    public class SendMessageAction
    {
        public ClientMessageCreate NewMessage { get; private set; }

        public SendMessageAction(ClientMessageCreate newMessage)
        {
            NewMessage = newMessage;
        }
    }

    public class SendMessageSuccessAction
    {
    }
}
