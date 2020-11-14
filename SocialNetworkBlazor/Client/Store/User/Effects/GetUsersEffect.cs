using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Users.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
{
    public class GetUsersEffect : Effect<GetUsersAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetUsersEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetUsersAction action, IDispatcher dispatcher)
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<ClientUser>>($"{_navigationManager.BaseUri}api/Users");

                dispatcher.Dispatch(new GetUsersSuccessAction(users));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new UserFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }

    }
}
