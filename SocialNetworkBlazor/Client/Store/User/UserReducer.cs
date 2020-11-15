using Fluxor;
using SocialNetworkBlazor.Client.Store.User.Actions;

namespace SocialNetworkBlazor.Client.Store.User
{
    public static class UserReducer
    {
        [ReducerMethod]
        public static UserState GetUsers(UserState state, GetUsersAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState GetUsersSuccess(UserState state, GetUsersSuccessAction action)
        {
            return state with
            {
                ClientUsers = action.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState UserFailed(UserState state, UserFailedAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState UpdateUserOnlineStatus(UserState state, UpdateUserOnlineStatusAction action)
        {
            var users = state.ClientUsers;
            foreach (var user in users)
            {
                if (user.ContactId == action.ContactId)
                {
                    user.IsOnline = action.IsOnline;
                }
            }

            return state with
            {
                ClientUsers = users
            };
        }

        [ReducerMethod]
        public static UserState UpdateUser(UserState state, UpdateUserSuccessAction action)
        {
            var users = state.ClientUsers;
            foreach (var user in state.ClientUsers)
            {
                if (user.ContactId == action.User.ContactId)
                {
                    user.FirstName = action.User.FirstName;
                    user.LastName = action.User.LastName;
                    user.ProfileImageTitle = action.User.ProfileImageTitle;
                }
            }
            return state with
            {
                ClientUsers = users
            };
        }
    }
}
