using Fluxor;
using SocialNetworkBlazor.Client.Store.Friendship.Actions;
using System.Linq;

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
            for (int i = 0; i < state.ClientFriendships.Count; i++)
            {
                if ((state.ClientFriendships[i].User1.Id == action.User1Id && state.ClientFriendships[i].User2.Id == action.User2Id)
                       || (state.ClientFriendships[i].User1.Id == action.User2Id && state.ClientFriendships[i].User2.Id == action.User1Id))
                {
                    state.ClientFriendships.Remove(state.ClientFriendships[i]);
                }
            }

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
        public static FriendshipState RecieveFriendship(FriendshipState state, RecieveFriendshipAction action)
        {
            state.ClientFriendships.Add(action.Friendship);
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState GetFriends(FriendshipState state, GetFriendsAction action)
        {
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState GetFriendsSuccess(FriendshipState state, GetFriendsSuccessAction action)
        {
            foreach (var friend in action.Friends)
            {
                if (state.ClientFriendships.Any(x => x.User1.Id == friend.User1.Id && x.User2.Id == friend.User2.Id))
                    continue;

                state.ClientFriendships.Add(friend);
            }
            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }

        [ReducerMethod]
        public static FriendshipState FriendshipUpdateUserOnlineStatus(FriendshipState state, FriendshipUpdateUserOnlineStatusAction action)
        {
            foreach (var friendship in state.ClientFriendships)
            {
                if (friendship.User1.ContactId == action.ContactId)
                    friendship.User1.IsOnline = action.IsOnline;

                if (friendship.User2.ContactId == action.ContactId)
                    friendship.User2.IsOnline = action.IsOnline;
            }

            return state with
            {
                ClientFriendships = state.ClientFriendships
            };
        }
    }
}
