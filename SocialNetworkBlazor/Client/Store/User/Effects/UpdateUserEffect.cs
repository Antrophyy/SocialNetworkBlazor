using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class UpdateUserEffect : Effect<UpdateUserAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public UpdateUserEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(UpdateUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_navigationManager.BaseUri}api/Users", action.User);

                var clientUserReceived = JsonSerializer.Deserialize<ClientUser>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                dispatcher.Dispatch(new UpdateUserSuccessAction(clientUserReceived));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new UserFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
