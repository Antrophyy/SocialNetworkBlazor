using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Post.Effects
{
    public class SendPostEffect : Effect<SendPostAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public SendPostEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(SendPostAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_navigationManager.BaseUri}api/Posts", action.NewPost);
                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new PostFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }
                var clientPost = JsonSerializer.Deserialize<ClientPost>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                dispatcher.Dispatch(new SendPostSuccessAction(clientPost));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new PostFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
