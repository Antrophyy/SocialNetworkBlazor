using Fluxor;
using SocialNetworkBlazor.Client.Store.Post.Actions;

namespace SocialNetworkBlazor.Client.Store.Post
{
    public static class PostReducer
    {
        [ReducerMethod]
        public static PostState GetPosts(PostState state, GetPostsAction action)
        {
            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }

        [ReducerMethod]
        public static PostState GetPostsSuccess(PostState state, GetPostsSuccessAction action)
        {
            return state with
            {
                ClientPosts = action.ClientPosts
            };
        }

        [ReducerMethod]
        public static PostState SendPost(PostState state, SendPostAction action)
        {
            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }

        [ReducerMethod]
        public static PostState SendPostSuccess(PostState state, SendPostSuccessAction action)
        {
            var posts = state.ClientPosts;
            posts.Insert(0, action.NewPost);
            return state with
            {
                ClientPosts = posts
            };
        }

        [ReducerMethod]
        public static PostState SendComment(PostState state, SendCommentAction action)
        {
            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }

        [ReducerMethod]
        public static PostState SendCommentSuccess(PostState state, SendCommentSuccessAction action)
        {
            foreach (var post in state.ClientPosts)
            {
                if (action.NewComment.PostId.HasValue)
                    post.Comments.Add(action.NewComment);
                else
                {
                    foreach (var comment in post.Comments)
                    {
                        if (comment.Id == action.NewComment.CommentId)
                        {
                            comment.Replies.Add(action.NewComment);
                        }
                    }
                }
            }
            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }
    }
}
