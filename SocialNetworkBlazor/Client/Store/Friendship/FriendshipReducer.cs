using Fluxor;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;

namespace SocialNetworkBlazor.Client.Store.Friendship
{
    public class FriendshipReducer
    {
        [ReducerMethod]
        public static FriendshipState DeleteFriendship(FriendshipState state, DeleteFriendshipAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState DeleteFriendshipSuccess(FriendshipState state, DeleteFriendshipSuccessAction action)
        {
            var friendships = state.ClientFriendships;
            friendships.Remove(action.DeletedFriendship);

            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState FriendshipFailed(FriendshipState state, FriendshipFailedAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState SendFriendship(FriendshipState state, SendFriendshipAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState UpdateFriendship(FriendshipState state, UpdateFriendshipAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState UpdateFriendshipSuccess(FriendshipState state, UpdateFriendshipSuccessAction action)
        {
            var friendships = state.ClientFriendships;
            foreach (var friendship in friendships)
            {
                if (friendship.User1.Id == action.Friendship.User1.Id && friendship.User2.Id == action.Friendship.User2.Id)
                {
                    friendship.Status = action.Friendship.Status;
                    break;
                }
            }

            return state with
            {
                ClientFriendships = friendships
            };
        }

        [ReducerMethod]
        public static FriendshipState GetFriendships(FriendshipState state, GetFriendshipsAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState GetFriendshipsSuccess(FriendshipState state, GetFriendshipsSuccessAction action)
        {
            return state with
            {
                ClientFriendships = action.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState RecieveFriendship(FriendshipState state, RecieveFriendshipAction action)
        {
            state.ClientFriendships.Add(action.Friendship);
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }
    }
}
