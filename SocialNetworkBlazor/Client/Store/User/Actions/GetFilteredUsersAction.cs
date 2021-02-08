using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class GetFilteredUsersAction
    {
        public string FilterString { get; private set; }
        public GetFilteredUsersAction(string filterString)
        {
            FilterString = filterString;
        }
    }

    public class GetFilteredUsersSuccessAction
    {
        public List<ClientUser> FoundUsers { get; private set; }
        public GetFilteredUsersSuccessAction(List<ClientUser> foundUsers)
        {
            FoundUsers = foundUsers;
        }
    }
}
