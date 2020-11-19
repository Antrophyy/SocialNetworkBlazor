using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using SocialNetworkBlazor.Shared.Models;
using System;
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
            var posts = await _httpClient.GetFromJsonAsync<List<ClientPost>>($"{_navigationManager.BaseUri}api/Posts/{action.UserId}");
            Console.WriteLine(posts.Count);
            posts.Sort((x, y) => y.PostedAt.CompareTo(x.PostedAt));

            dispatcher.Dispatch(new GetPostsSuccessAction(posts));
        }
    }
}
