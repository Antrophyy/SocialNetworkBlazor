using Fluxor;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Client.Store.User.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class UpdateUserOnlineStatusEffect : Effect<UpdateUserOnlineStatusAction>
    {
        protected override async Task HandleAsync(UpdateUserOnlineStatusAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new FriendshipUpdateUserOnlineStatusAction(action.IsOnline, action.ContactId));
        }
    }
}
