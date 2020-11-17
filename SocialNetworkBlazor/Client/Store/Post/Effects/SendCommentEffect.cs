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
    public class SendCommentEffect : Effect<SendCommentAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public SendCommentEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(SendCommentAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_navigationManager.BaseUri}api/Comments", action.NewComment);
                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new PostFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }
                var clientPost = JsonSerializer.Deserialize<ClientComment>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                dispatcher.Dispatch(new SendCommentSuccessAction(clientPost));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new PostFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
