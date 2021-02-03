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
    public partial class CreateNewPostComment
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Parameter]
        public int ParentPostId { get; set; }
        [Parameter]
        public ClientComment Comment { get; set; }
        [Parameter]
        public EventCallback<bool> CommentSent { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<UserState> UserState { get; set; }
        public ClientCommentCreate ClientCommentCreated { get; set; } = new ClientCommentCreate();
        public ClientUser LoggedInUser { get; set; }
        public ClaimsPrincipal UserClaims { get; set; }
        
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
                StateHasChanged();
            };

            await base.OnInitializedAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (LoggedInUser == null)
            {
                int contactId = int.Parse(UserClaims.Claims.Single(x => x.Type == "ContactId").Value);
                LoggedInUser = UserState.Value.ClientUsers.Where(x => x.ContactId == contactId).First();
                StateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            if (Comment != null)
                ClientCommentCreated.Content = $"{Comment.Author.FullName}, ";
            else
                ClientCommentCreated.Content = "";
        }

        public void HandleValidSubmit()
        {
            ClientCommentCreated.AuthorId = LoggedInUser.Id;
            if (Comment != null)
            {
                ClientCommentCreated.CommentId = Comment.CommentId == null ? Comment.Id : Comment.CommentId;
            }
            else
            {
                ClientCommentCreated.PostId = ParentPostId;
            }

            Dispatcher.Dispatch(new SendCommentAction(ClientCommentCreated));
            ClientCommentCreated = new ClientCommentCreate();
            CommentSent.InvokeAsync(true);
        }
    }
}
