using Fluxor;
using Microsoft.AspNetCore.Components;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Client.Components
{
    public partial class FeedPost
    {
        [Inject]
        public IDispatcher Dispatcher { get; set; }

        [Parameter]
        public ClientPost Post { get; set; }

        public string TimePassedString { get; set; }

        public bool AreCommentsExpanded { get; set; } = false;

        public bool IsReplyClicked { get; set; } = false;

        public ClientComment CommentReceived { get; set; }

        protected override void OnParametersSet()
        {
            GetLastPosted();

            base.OnParametersSet();
        }

        private void GetLastPosted()
        {
            TimeSpan time = DateTimeOffset.UtcNow.Subtract(Post.PostedAt);

            Console.WriteLine($"M{time.TotalMinutes}, H{time.TotalHours}");

            if (time.TotalMinutes < 60)
                TimePassedString = time.Minutes.ToString() + "m";
            else if (time.TotalHours < 24)
                TimePassedString = time.Hours.ToString() + "h";
            else if (time.TotalDays < 365)
                TimePassedString = time.Days.ToString() + "d";
            else
                TimePassedString = time.Days / 365 + "y";
        }

        public void HandleCommentsExpansion()
        {
            AreCommentsExpanded = !AreCommentsExpanded;
        }

        public void HandleReplyClicked(ClientComment comment)
        {
            IsReplyClicked = !IsReplyClicked;
            CommentReceived = comment;
        }
    }
}
