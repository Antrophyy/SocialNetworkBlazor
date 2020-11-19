using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Post.Actions
{
    public class GetPostsAction
    {
        public string UserId { get; private set; }
        public GetPostsAction(string userId)
        {
            UserId = userId;
        }
    }

    public class GetPostsSuccessAction
    {
        public List<ClientPost> ClientPosts { get; private set; }

        public GetPostsSuccessAction(List<ClientPost> posts)
        {
            ClientPosts = posts;
        }
    }
}
