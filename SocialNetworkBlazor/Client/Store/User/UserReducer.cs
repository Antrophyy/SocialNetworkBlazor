using Fluxor;
using SocialNetworkBlazor.Client.Store.Users.Actions;

namespace SocialNetworkBlazor.Client.Store.Users
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
    }
}
