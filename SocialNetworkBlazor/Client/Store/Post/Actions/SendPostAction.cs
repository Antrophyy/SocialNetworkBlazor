using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Post.Actions
{
    public class SendPostAction
    {
        public ClientPostCreate NewPost { get; private set; }

        public SendPostAction(ClientPostCreate newPost)
        {
            NewPost = newPost;
        }
    }

    public class SendPostSuccessAction
    {
        public ClientPost NewPost { get; private set; }
        
        public SendPostSuccessAction(ClientPost newPost)
        {
            NewPost = newPost;
        }
    }
}
