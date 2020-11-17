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
    public class GetPostsEffect : Effect<GetPostsAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetPostsEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetPostsAction action, IDispatcher dispatcher)
        {
            try
            {
                var posts = await _httpClient.GetFromJsonAsync<List<ClientPost>>($"{_navigationManager.BaseUri}api/Posts");
                posts.Sort((x, y) => y.PostedAt.CompareTo(x.PostedAt));

                dispatcher.Dispatch(new GetPostsSuccessAction(posts));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new PostFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
