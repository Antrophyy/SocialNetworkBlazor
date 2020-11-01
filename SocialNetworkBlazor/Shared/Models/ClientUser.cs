namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Email { get; set; }
    }
}
