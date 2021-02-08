using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class GetFilteredUsersEffect : Effect<GetFilteredUsersAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetFilteredUsersEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetFilteredUsersAction action, IDispatcher dispatcher)
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<ClientUser>>($"{_navigationManager.BaseUri}api/Users/GetFilteredUsers/{action.FilterString}");

                dispatcher.Dispatch(new GetFilteredUsersSuccessAction(users));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new UserFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
