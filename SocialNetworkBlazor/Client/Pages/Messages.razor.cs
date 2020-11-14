using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Client.Store.Messages;
using SocialNetworkBlazor.Client.Store.Messages.Actions;
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
        public IDispatcher Dispatcher { get; set; }

        public ClientMessageCreate NewMessage { get; set; } = new ClientMessageCreate();

        public ClaimsPrincipal LoggedInUser { get; set; }

        private HubConnection _signalRConnection;

        protected override async Task OnParametersSetAsync()
        {
            var state = await AuthenticationState;
            LoggedInUser = state.User;

            Dispatcher.Dispatch(new GetMessagesAction(ContactId));
            
            NewMessage = new ClientMessageCreate();
        }

        protected override async Task OnInitializedAsync()
        {
            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<ClientMessage>("message", m =>
            {
                MessageState.Value.ClientMessages.Insert(0, m);
                StateHasChanged();
            });
        }

        private void HandleCompleteCreate()
        {
            NewMessage.AuthorID = LoggedInUser.Claims.First(x => x.Type == "sub").Value;
            NewMessage.RecipientContactId = ContactId;

            Dispatcher.Dispatch(new AddMessageAction(NewMessage));

            NewMessage = new ClientMessageCreate();
        }

        public async ValueTask DisposeAsync() => await _signalRConnection.DisposeAsync();
    }
}
