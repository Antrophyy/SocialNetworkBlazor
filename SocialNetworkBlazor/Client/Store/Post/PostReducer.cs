using Fluxor;
using SocialNetworkBlazor.Client.Store.Post.Actions;
using System.Linq;

namespace SocialNetworkBlazor.Client.Store.Post
{
    public static class PostReducer
    {
        [ReducerMethod]
        public static PostState GetPostsSingleUser(PostState state, GetPostsSingleUserAction action)
        {
            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }

        [ReducerMethod]
        public static PostState GetPostsSingleUserSucces(PostState state, GetPostsSingleUserSuccessAction action)
        {
            foreach (var post in action.UserPosts)
            {
                if (state.ClientPosts.Any(x => x.Id == post.Id))
                    continue;

                state.ClientPosts.Add(post);
            }

            return state with
            {
                ClientPosts = state.ClientPosts
            };
        }
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
            foreach (var post in action.ClientPosts)
            {
                if (state.ClientPosts.Any(x=>x.Id == post.Id))
                    continue;

                state.ClientPosts.Add(post);
            }
            return state with
            {
                ClientPosts = state.ClientPosts
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
                if (action.NewComment.PostId.HasValue && action.NewComment.PostId == post.Id)
                    post.Comments.Insert(0, action.NewComment);
                else
                {
                    foreach (var comment in post.Comments)
                    {
                        if (comment.Id == action.NewComment.CommentId)
                            comment.Replies.Insert(0, action.NewComment);
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
