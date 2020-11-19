using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Friendship.Effects
{
    public class GetFriendshipsEffect : Effect<GetFriendshipsAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetFriendshipsEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetFriendshipsAction action, IDispatcher dispatcher)
        {
            try
            {
                var friendships = await _httpClient.GetFromJsonAsync<List<ClientFriendship>>($"{_navigationManager.BaseUri}api/Friendships/{action.UserId}");

                dispatcher.Dispatch(new GetFriendshipsSuccessAction(friendships));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new FriendshipFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
