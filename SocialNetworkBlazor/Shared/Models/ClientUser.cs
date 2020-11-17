namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Email { get; set; }
        public int ContactId { get; set; }
        public bool IsOnline { get; set; }
        public string ProfileImageTitle { get; set; }
    }
}
