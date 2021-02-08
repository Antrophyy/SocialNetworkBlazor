using Fluxor;
using SocialNetworkBlazor.Client.Store.User.Actions;
using System.Linq;

namespace SocialNetworkBlazor.Client.Store.User
{
    public static class UserReducer
    {
        [ReducerMethod]
        public static UserState GetSingleUser(UserState state, GetSingleUserAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers,
            };
        }

        [ReducerMethod]
        public static UserState GetSingleUserSucces(UserState state, GetSingleUserSuccessAction action)
        {
            if (!state.ClientUsers.Any(x => x.Id == action.User.Id))
                state.ClientUsers.Add(action.User);


            return state with
            {
                ClientUsers = state.ClientUsers,
            };
        }

        [ReducerMethod]
        public static UserState GetUsers(UserState state, GetUsersAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers,
            };
        }

        [ReducerMethod]
        public static UserState GetFilteredUsers(UserState state, GetFilteredUsersAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState GetFilteredUsersSuccess(UserState state, GetFilteredUsersSuccessAction action)
        {
            foreach (var user in action.FoundUsers)
            {
                if (state.ClientUsers.Any(x => x.Id == user.Id))
                    continue;

                state.ClientUsers.Add(user);
            }

            return state with
            {
                ClientUsers = state.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState GetUsersSuccess(UserState state, GetUsersSuccessAction action)
        {
            foreach (var user in action.ClientUsers)
            {
                if (state.ClientUsers.Any(x=>x.Id == user.Id))
                    continue;

                state.ClientUsers.Add(user);
            }
            return state with
            {
                ClientUsers = state.ClientUsers,
            };
        }

        [ReducerMethod]
        public static UserState UserFailed(UserState state, UserFailedAction action)
        {
            return state with
            {
                ClientUsers = state.ClientUsers,
            };
        }

        [ReducerMethod]
        public static UserState UpdateUserOnlineStatus(UserState state, UpdateUserOnlineStatusAction action)
        {
            foreach (var user in state.ClientUsers)
            {
                if (user.ContactId == action.ContactId)
                    user.IsOnline = action.IsOnline;
            }

            return state with
            {
                ClientUsers = state.ClientUsers
            };
        }

        [ReducerMethod]
        public static UserState UpdateUser(UserState state, UpdateUserSuccessAction action)
        {
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
                ClientUsers = state.ClientUsers,
            };
        }
    }
}
