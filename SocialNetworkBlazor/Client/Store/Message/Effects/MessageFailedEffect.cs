using Fluxor;
using SocialNetworkBlazor.Client.Store.Message.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Message.Effect
{
    public class MessageFailedEffect : Effect<MessageFailedAction>
    {
        protected override Task HandleAsync(MessageFailedAction action, IDispatcher dispatcher)
        {
            // show toast
            return Task.CompletedTask;
        }
    }
}
