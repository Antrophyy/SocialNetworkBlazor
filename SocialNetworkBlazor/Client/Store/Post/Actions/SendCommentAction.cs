using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.Post.Actions
{
    public class SendCommentAction
    {
        public ClientCommentCreate NewComment { get; private set; }

        public SendCommentAction(ClientCommentCreate newComment)
        {
            NewComment = newComment;
        }
    }

    public class SendCommentSuccessAction
    {
        public ClientComment NewComment { get; private set; }

        public SendCommentSuccessAction(ClientComment newPost)
        {
            NewComment = newPost;
        }
    }
}
