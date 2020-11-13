using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
        protected HttpClient HttpClient { get; set; }

        public List<ClientMessage> AllMessages { get; set; } = new List<ClientMessage>();

        public InputTextArea TextArea { get; set; }

        public ClientMessageCreate NewMessage { get; set; } = new ClientMessageCreate();

        public ClaimsPrincipal LoggedInUser { get; set; }

        public EditForm EditForm { get; set; }

        private HubConnection _signalRConnection;

        protected override async Task OnParametersSetAsync()
        {
            AllMessages = await HttpClient.GetFromJsonAsync<List<ClientMessage>>($"{NavigationManager.BaseUri}api/messages/{ContactId}");
            AllMessages.Sort((x, y) => y.SentAt.Value.CompareTo(x.SentAt.Value));
            NewMessage = new ClientMessageCreate();

            var state = await AuthenticationState;
            LoggedInUser = state.User;
        }

        protected override async Task OnInitializedAsync()
        {
            _signalRConnection = new HubConnectionBuilder().WithUrl(NavigationManager.BaseUri.ToString() + "notificationhub").Build();

            await _signalRConnection.StartAsync();

            _signalRConnection.On<ClientMessage>("message", m =>
            {
                AllMessages.Insert(0, m);
                StateHasChanged();
            });
        }

        private async Task HandleCompleteCreate()
        {
            var auth = await AuthenticationState;
            NewMessage.AuthorID = auth.User.Claims.First(x => x.Type == "sub").Value;
            NewMessage.RecipientContactId = ContactId;
            var response = await HttpClient.PostAsJsonAsync($"{NavigationManager.BaseUri}api/Messages", NewMessage);

            if (!response.IsSuccessStatusCode)
                return;

            NewMessage = new ClientMessageCreate();
        }

        public async ValueTask DisposeAsync() => await _signalRConnection.DisposeAsync();

    }
}
