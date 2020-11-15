using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class GetUsersAction
    {
    }

    public class GetUsersSuccessAction
    {
        public List<ClientUser> ClientUsers { get; private set; }

        public GetUsersSuccessAction(List<ClientUser> users)
        {
            ClientUsers = users;
        }
    }
}
