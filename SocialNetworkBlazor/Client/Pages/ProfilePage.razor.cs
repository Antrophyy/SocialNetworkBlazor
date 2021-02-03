using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SocialNetworkBlazor.Client.Store.Friendship;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using SocialNetworkBlazor.Client.Store.Post;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using SocialNetworkBlazor.Client.Store.User;
using SocialNetworkBlazor.Client.Store.User.Actions;
using SocialNetworkBlazor.Shared.Helpers;
using SocialNetworkBlazor.Shared.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Pages
{
    public partial class ProfilePage
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Parameter]
        public int ContactId { get; set; }
        [Inject]
        private IState<UserState> UserState { get; set; }
        [Inject]
        private IState<PostState> PostState { get; set; }
        [Inject]
        private IState<FriendshipState> FriendshipState { get; set; }
        [Inject]
        private IDispatcher Dispatcher { get; set; }
        private ClientUser _selectedUser;
        public ClaimsPrincipal UserClaims { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            UserClaims = state.User;

            if (!UserClaims.Identity.IsAuthenticated)
                return;

            await LoadUserProfile();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            _selectedUser = null;
            
            await LoadUserProfile();
            await base.OnParametersSetAsync();
        }

        private async Task LoadUserProfile()
        {
            await LoadUser();
            await LoadPosts();
        }

        private async Task LoadUser()
        {
            _selectedUser = UserState.Value.ClientUsers.Where(x => x.ContactId == ContactId).SingleOrDefault();
            if (_selectedUser == null)
                Dispatcher.Dispatch(new GetSingleUserAction(ContactId));

            UserState.StateChanged += (sender, state) =>
            {
                if (_selectedUser != null)
                    return;

                if (state.ClientUsers.Where(x => x.ContactId == ContactId).SingleOrDefault() == null)
                    return;

                _selectedUser = state.ClientUsers.Where(x => x.ContactId == ContactId).SingleOrDefault();
            };
        }

        private async Task LoadPosts()
        {
            Dispatcher.Dispatch(new GetPostsSingleUserAction(ContactId));
        }

        private void HandleRemoveFriendClicked()
        {
            Dispatcher.Dispatch(new DeleteFriendshipAction(_selectedUser.Id, UserClaims.Claims.Single(x => x.Type == "sub").Value));
        }

        private void HandleAddFriendClicked()
        {
            var newFriendship = new ClientFriendshipCreate
            {
                Status = RelationshipStatusConstants.PENDING,
                User1 = UserState.Value.ClientUsers.Where(x => x.Id == UserClaims.Claims.Single(x => x.Type == "sub").Value).Single(),
                User2 = _selectedUser
            };

            Dispatcher.Dispatch(new SendFriendshipAction(newFriendship));
        }
    }
}
