using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Shared.Models;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Components
{
    public partial class PostComment
    {
        [Parameter]
        public ClientComment Comment { get; set; }

        [Parameter]
        public int ParentPostId { get; set; }
        public bool AreRepliesShown { get; set; } = false;
        public bool IsReplyClicked { get; set; } = false;
        public void HandleRepliesExpansion() => AreRepliesShown = !AreRepliesShown;

        public void HandleReplyClicked()
        {
            IsReplyClicked = !IsReplyClicked;
        }

        public void HandleCommentSent(bool value)
        {
            HandleReplyClicked();
        }
    }
}
