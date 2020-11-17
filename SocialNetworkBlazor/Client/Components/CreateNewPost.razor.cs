using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Shared.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Components
{
    public partial class CreateNewPost
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<UserState> UserState { get; set; }
        public ClientPostCreate ClientPostCreated { get; set; } = new ClientPostCreate();
        public ClaimsPrincipal UserClaims { get; set; }
        public ClientUser LoggedInUser { get; set; }
        public string PlaceholderText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            UserClaims = state.User;

            UserState.StateChanged += delegate
            {
                if (LoggedInUser != null)
                    return;

                if (UserState.Value.ClientUsers.Count == 0)
                    return;

                int contactId = int.Parse(UserClaims.Claims.Single(x => x.Type == "ContactId").Value);
                LoggedInUser = UserState.Value.ClientUsers.Where(x => x.ContactId == contactId).First();
                PlaceholderText = $"What's on your mind, {LoggedInUser.FirstName}?";
                StateHasChanged();
            };
        }

        public void HandleValidSubmit()
        {
            ClientPostCreated.AuthorId = LoggedInUser.Id;
            Dispatcher.Dispatch(new SendPostAction(ClientPostCreated));
            ClientPostCreated = new ClientPostCreate();
        }
    }
}
