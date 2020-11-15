using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Message.Actions
{
    public class GetMessagesAction
    {
        public int ContactId { get; private set; }
        public GetMessagesAction(int contactId)
        {
            ContactId = contactId;
        }
    }

    public class GetMessagesSuccessAction
    {
        public List<ClientMessage> ClientMessages { get; private set; }
        public int ContactId { get; private set; }

        public GetMessagesSuccessAction(List<ClientMessage> messages)
        {
            ClientMessages = messages;
        }
    }
}


