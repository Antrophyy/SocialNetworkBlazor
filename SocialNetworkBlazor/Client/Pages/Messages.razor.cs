using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Client.Store.Message;
using SocialNetworkBlazor.Client.Store.Message.Actions;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Pages
{
    public partial class Messages : IAsyncDisposable
    {
        [Parameter]
        public int ContactId { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        private IState<MessageState> MessageState { get; set; }

        [Inject]
        private IState<UserState> UserState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        public ClientUser SelectedUser { get; set; }

        public string PageTitle { get; set; }

        public ClientMessageCreate NewMessage { get; set; } = new ClientMessageCreate();

        public ClaimsPrincipal LoggedInUser { get; set; }

        private HubConnection _signalRConnection;

        protected override async Task OnParametersSetAsync()
        {
            var state = await AuthenticationState;
            LoggedInUser = state.User;

            if (!LoggedInUser.Identity.IsAuthenticated)
                return;

            Dispatcher.Dispatch(new GetMessagesAction(ContactId));
            Dispatcher.Dispatch(new GetSingleUserAction(ContactId));

            UserState.StateChanged += (sender, state) =>
            {
                if (SelectedUser != null)
                    return;

                if (state.ClientUsers.Where(x => x.ContactId == ContactId).SingleOrDefault() == null)
                    return;

                SelectedUser = state.ClientUsers.Where(x => x.ContactId == ContactId).SingleOrDefault();
                PageTitle = $"Messages with {SelectedUser.FullName}";
            };

            NewMessage = new ClientMessageCreate();

            await base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<ClientMessage>("message", m =>
            {
                Dispatcher.Dispatch(new RecieveMessageAction(m));
            });

            await base.OnInitializedAsync();
        }

        private void HandleCompleteCreate()
        {
            NewMessage.AuthorID = LoggedInUser.Claims.First(x => x.Type == "sub").Value;
            NewMessage.RecipientContactId = ContactId;

            Dispatcher.Dispatch(new SendMessageAction(NewMessage));

            NewMessage = new ClientMessageCreate();
        }

        public async ValueTask DisposeAsync() => await _signalRConnection.DisposeAsync();
    }
}
