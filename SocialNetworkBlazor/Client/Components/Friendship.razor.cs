using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Client.Store.Friendship;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Shared.Helpers;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Components
{
    public partial class Friendship
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        private IState<FriendshipState> FriendshipState { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }
        [Inject]
        private IDispatcher Dispatcher { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private HubConnection _signalRConnection;
        private ClientUser LoggedInUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;

            UserState.StateChanged += delegate
            {
                if (LoggedInUser != null)
                    return;

                if (UserState.Value.ClientUsers.Count == 0)
                    return;

                var _userId = state.User.Claims.Single(x => x.Type == "sub").Value;

                LoggedInUser = UserState.Value.ClientUsers.Where(x => x.Id == _userId).Single();
                Dispatcher.Dispatch(new GetFriendshipsAction(_userId));
            };

            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<ClientFriendship>("friendRequest", fr =>
            {
                if (fr.User2.Id == LoggedInUser.Id)
                    Dispatcher.Dispatch(new RecieveFriendshipAction(fr));
            });
        }

        private void HandleDeclineRequestButton(ClientFriendship friendshipToDecline)
        {
            Dispatcher.Dispatch(new DeleteFriendshipAction(friendshipToDecline));
        }

        private void HandleAcceptRequestButton(ClientFriendship friendstipToAccept)
        {
            var friendship = new ClientFriendshipUpdate()
            {
                User1 = friendstipToAccept.User1,
                User2 = friendstipToAccept.User2,
                Status = RelationshipStatusConstants.ACCEPTED
            };
            Dispatcher.Dispatch(new UpdateFriendshipAction(friendship));
        }
    }
}
