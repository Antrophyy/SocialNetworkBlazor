using Fluxor;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Post
{
    public record PostState
    {
        public List<ClientPost> ClientPosts { get; init; }
    }

    public class PostFeatureState : Feature<PostState>
    {
        public override string GetName() => nameof(PostState);

        protected override PostState GetInitialState()
        {
            return new PostState
            {
                ClientPosts = new List<ClientPost>()
            };
        }
    }
}
