using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Friendship.Effects
{
    public class SendFriendshipEffect : Effect<SendFriendshipAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public SendFriendshipEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(SendFriendshipAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_navigationManager.BaseUri}api/Friendships", action.NewFriendship);

                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new FriendshipFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }

                dispatcher.Dispatch(new SendFriendshipSuccessAction());
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new FriendshipFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
