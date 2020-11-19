namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientFriendshipUpdate
    {
        public ClientUser User1 { get; set; }
        public ClientUser User2 { get; set; }
        public string Status { get; set; }
    }
}
