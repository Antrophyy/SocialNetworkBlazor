using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Components.UserProfile
{
    public partial class ProfilePost
    {
        [Parameter]
        public ClientPost Post { get; set; }

        public bool AreCommentsExpanded { get; set; } = false;

        public void HandleCommentsExpansion()
        {
            AreCommentsExpanded = !AreCommentsExpanded;
        }
    }
}
