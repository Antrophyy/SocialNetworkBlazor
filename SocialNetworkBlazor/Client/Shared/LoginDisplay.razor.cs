using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Shared
{
    public partial class LoginDisplay
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationState;
            if (!state.User.Identity.IsAuthenticated)
                return;
        }

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("authentication/logout");
        }
    }
}
