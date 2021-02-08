using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SocialNetworkBlazor.Client.Store.Post;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Pages
{
    public partial class Index
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        private IState<PostState> PostState { get; set; }
        
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            if (!state.User.Identity.IsAuthenticated)
                return;

            Dispatcher.Dispatch(new GetPostsAction("cf7bf6b8-0089-4b4b-86fd-6f4a3bf2a692"));

            await base.OnInitializedAsync();
        }
    }
}
