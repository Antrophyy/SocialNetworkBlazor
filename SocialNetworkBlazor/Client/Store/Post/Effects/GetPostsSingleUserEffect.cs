using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Post.Effects
{
    public class GetPostsSingleUserEffect : Effect<GetPostsSingleUserAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetPostsSingleUserEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetPostsSingleUserAction action, IDispatcher dispatcher)
        {
            try
            {
                var userPosts = await _httpClient.GetFromJsonAsync<List<ClientPost>>($"{_navigationManager.BaseUri}api/Posts/GetPostsSingleUser/{action.ContactId}");

                dispatcher.Dispatch(new GetPostsSingleUserSuccessAction(userPosts));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new PostFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
