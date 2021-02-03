using Fluxor;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.User
{
    public record UserState
    {
        public List<ClientUser> ClientUsers { get; init; }
    }

    public class UserFeatureState : Feature<UserState>
    {
        public override string GetName() => nameof(UserState);

        protected override UserState GetInitialState()
        {
            return new UserState
            {
                ClientUsers = new List<ClientUser>(),
            };
        }
    }
}
