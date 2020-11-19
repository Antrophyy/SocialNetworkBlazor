using Fluxor;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Friendship
{
    public record FriendshipState
    {
        public List<ClientFriendship> ClientFriendships { get; init; }
    }

    public class FriendshipFeatureState : Feature<FriendshipState>
    {
        public override string GetName() => nameof(FriendshipState);

        protected override FriendshipState GetInitialState()
        {
            return new FriendshipState
            {
                ClientFriendships = new List<ClientFriendship>()
            };
        }
    }
}
