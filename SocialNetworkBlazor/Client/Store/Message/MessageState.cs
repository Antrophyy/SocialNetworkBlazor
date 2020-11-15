using Fluxor;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Message
{
    public record MessageState
    {
        public List<ClientMessage> ClientMessages { get; init; }
    }

    public class MessageFeatureState : Feature<MessageState>
    {
        public override string GetName() => nameof(MessageState);

        protected override MessageState GetInitialState()
        {
            return new MessageState
            {
                ClientMessages = new List<ClientMessage>()
            };
        }
    }
}
