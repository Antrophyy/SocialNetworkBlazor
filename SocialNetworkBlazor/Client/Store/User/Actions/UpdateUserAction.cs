using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class UpdateUserAction
    {
        public ClientUserUpdate User { get; private set; }

        public UpdateUserAction(ClientUserUpdate user)
        {
            User = user;
        }
    }

    public class UpdateUserSuccessAction
    {
        public ClientUser User { get; private set; }

        public UpdateUserSuccessAction(ClientUser updatedUser)
        {
            User = updatedUser;
        }
    }
}
