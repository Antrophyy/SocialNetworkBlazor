using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Client.Store.Friendship;
using SocialNetworkBlazor.Client.Store.User.Actions;

namespace SocialNetworkBlazor.Client.Shared
{
    public partial class FriendsList : IAsyncDisposable
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        private IState<FriendshipState> FriendshipState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private HubConnection _signalRConnection;
        private string _userId;

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            var user = state.User;

            if (!user.Identity.IsAuthenticated)
                return;

            _userId = user.Claims.Single(x => x.Type == "sub").Value;
            if (user.Identity.IsAuthenticated && _signalRConnection == null)
                await EnableSignalRConnection();

            Dispatcher.Dispatch(new GetFriendsAction(_userId));

            FriendshipState.StateChanged += (sender, state) =>
            {
                if (state.ClientFriendships.Count == 0)
                    return;

                StateHasChanged();
            };
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_signalRConnection == null)
                await EnableSignalRConnection();
        }

        private async Task EnableSignalRConnection()
        {
            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<StatusChange>("statusChange", s =>
            {
                Dispatcher.Dispatch(new UpdateUserOnlineStatusAction(s.IsOnline, s.ContactId));
            });
        }

        private static string GetOnlineStatus(ClientUser user) => user.IsOnline ? "user_online.png" : "user_offline.png";

        public async ValueTask DisposeAsync() => await _signalRConnection.DisposeAsync();
    }
}
