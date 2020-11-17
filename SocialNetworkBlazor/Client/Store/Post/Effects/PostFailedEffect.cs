using Fluxor;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Post.Effects
{
    public class PostFailedEffect : Effect<PostFailedAction>
    {
        protected override Task HandleAsync(PostFailedAction action, IDispatcher dispatcher)
        {
            // show toast
            return Task.CompletedTask;
        }
    }
}
