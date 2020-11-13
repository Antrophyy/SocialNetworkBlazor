using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Shared
{
    public partial class FriendsList : IAsyncDisposable
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }

        private HubConnection _signalRConnection;

        public List<ClientUser> Users { get; set; } = new List<ClientUser>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await Http.GetFromJsonAsync<List<ClientUser>>("api/Users");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

            var state = await AuthenticationState;
            var user = state.User;

            if (user.Identity.IsAuthenticated)
                await EnableSignalRConnection();
        }

        private async Task EnableSignalRConnection()
        {
            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<StatusChange>("statusChange", s =>
            {
                foreach (var user in Users)
                {
                    if (user.ContactId == s.ContactId)
                    {
                        user.IsOnline = s.IsOnline;
                        StateHasChanged();
                        break;
                    }
                }
            });
        }

        private static string GetOnlineStatus(ClientUser user) => user.IsOnline ? "user_online.png" : "user_offline.png";

        public async ValueTask DisposeAsync() => await _signalRConnection.DisposeAsync();
    }
}
