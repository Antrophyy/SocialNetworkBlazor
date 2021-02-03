using Fluxor;
using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Friendship.Effects
{
    public class GetFriendsEffect : Effect<GetFriendsAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetFriendsEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetFriendsAction action, IDispatcher dispatcher)
        {
                var friends = await _httpClient.GetFromJsonAsync<List<ClientFriendship>>($"{_navigationManager.BaseUri}api/Friendships/{action.UserId}");

                dispatcher.Dispatch(new GetFriendsSuccessAction(friends));
        }
    }
}
