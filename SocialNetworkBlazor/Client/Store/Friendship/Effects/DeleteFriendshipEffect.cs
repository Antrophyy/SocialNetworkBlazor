using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Friendship.Effects
{
    public class DeleteFriendshipEffect : Effect<DeleteFriendshipAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public DeleteFriendshipEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(DeleteFriendshipAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(
                    $"{_navigationManager.BaseUri}api/Friendships/{action.User1Id}/{action.User2Id}");

                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new FriendshipFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }

                dispatcher.Dispatch(new DeleteFriendshipSuccessAction(action.User1Id, action.User2Id));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new FriendshipFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
