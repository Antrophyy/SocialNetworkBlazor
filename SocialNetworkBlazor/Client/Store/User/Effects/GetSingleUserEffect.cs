using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class GetSingleUserEffect : Effect<GetSingleUserAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetSingleUserEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetSingleUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<ClientUser>($"{_navigationManager.BaseUri}api/Users/{action.ContactId}");

                dispatcher.Dispatch(new GetSingleUserSuccessAction(user));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new UserFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
