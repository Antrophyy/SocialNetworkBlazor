using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SocialNetworkBlazor.Client.Store.Message.Actions;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Store.Message.Effect
{
    public class GetMessagesEffect : Effect<GetMessagesAction>
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GetMessagesEffect(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task HandleAsync(GetMessagesAction action, IDispatcher dispatcher)
        {
            try
            {
                var messages = await _httpClient.GetFromJsonAsync<List<ClientMessage>>($"{_navigationManager.BaseUri}api/Messages/{action.ContactId}");
                messages.Sort((x, y) => y.SentAt.Value.CompareTo(x.SentAt.Value));

                dispatcher.Dispatch(new GetMessagesSuccessAction(messages));
            }
            catch (AccessTokenNotAvailableException e)
            {
                dispatcher.Dispatch(new MessageFailedAction(errorTitle: "Chyba!", errorMessage: $"Vyskytla se chyba: {e.Message}"));
                e.Redirect();
            }
        }
    }
}
