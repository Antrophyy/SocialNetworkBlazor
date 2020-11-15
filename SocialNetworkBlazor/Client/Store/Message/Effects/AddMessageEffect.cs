using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Message.Actions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Message.Effect
{
    public class AddMessageEffect : Effect<SendMessageAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public AddMessageEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(SendMessageAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_navigationManager.BaseUri}api/Messages", action.NewMessage);
                if (!response.IsSuccessStatusCode)
                {
                    dispatcher.Dispatch(new MessageFailedAction(response.StatusCode.ToString(), response.ReasonPhrase));
                    return;
                }

                dispatcher.Dispatch(new SendMessageSuccessAction());
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new MessageFailedAction("Chyba!", e.Message));
                e.Redirect();
            }
        }
    }
}
