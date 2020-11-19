using Fluxor;
using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.User.Effects
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
                var friends = await _httpClient.GetFromJsonAsync<List<ClientUser>>($"{_navigationManager.BaseUri}api/Users/GetUsersFriends/{action.UserId}");

                dispatcher.Dispatch(new GetFriendsSuccessAction(friends));
        }
    }
}
