using Fluxor;
using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Client.Store.Post;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private IState<PostState> PostState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Dispatcher.Dispatch(new GetPostsAction("cf7bf6b8-0089-4b4b-86fd-6f4a3bf2a692"));
        }
    }
}
