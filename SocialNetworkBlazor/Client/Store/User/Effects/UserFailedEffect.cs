using Fluxor;
using SocialNetworkBlazor.Client.Store.User.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class UserFailedEffect : Effect<UserFailedAction>
    {
        protected override Task HandleAsync(UserFailedAction action, IDispatcher dispatcher)
        {
            // show toast
            return Task.CompletedTask;
        }
    }
}
