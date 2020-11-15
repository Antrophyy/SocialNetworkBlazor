using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Message.Actions
{
    public class RecieveMessageAction
    {
        public ClientMessage Message { get; private set; }

        public RecieveMessageAction(ClientMessage message)
        {
            Message = message; 
        }
    }
}
