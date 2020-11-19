using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Friendship.Effects
{
    public class UpdateFriendshipEffect : Effect<UpdateFriendshipAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public UpdateFriendshipEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(UpdateFriendshipAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_navigationManager.BaseUri}api/Friendships", action.UpdatedFriendship);

                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new FriendshipFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }

                var clientFriendship = JsonSerializer.Deserialize<ClientFriendship>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                dispatcher.Dispatch(new UpdateFriendshipSuccessAction(clientFriendship));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new FriendshipFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
