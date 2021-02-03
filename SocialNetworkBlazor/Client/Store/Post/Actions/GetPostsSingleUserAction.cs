using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.Post.Actions
{
    public class GetPostsSingleUserAction
    {
        public int ContactId { get; private set; }
        public GetPostsSingleUserAction(int contactId)
        {
            ContactId = contactId;
        }
    }

    public class GetPostsSingleUserSuccessAction
    {
        public List<ClientPost> UserPosts { get; private set; }
        public GetPostsSingleUserSuccessAction(List<ClientPost> userPosts)
        {
            UserPosts = userPosts;
        }
    }
}
