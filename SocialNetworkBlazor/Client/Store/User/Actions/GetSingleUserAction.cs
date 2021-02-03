using SocialNetworkBlazor.Shared.Models;

namespace SocialNetworkBlazor.Client.Store.User.Actions
{
    public class GetSingleUserAction
    {
        public int ContactId { get; private set; }
        public GetSingleUserAction(int contactId)
        {
            ContactId = contactId;
        }

    }

    public class GetSingleUserSuccessAction
    {
        public ClientUser User { get; private set; }
        public GetSingleUserSuccessAction(ClientUser user)
        {
            User = user;
        }
    }
}
