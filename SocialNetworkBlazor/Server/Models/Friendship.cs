using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Server.Models
{
    public class Friendship
    {
        public User User1 { get; set; }
        [Required]
        public string User1Id { get; set; }
        public User User2 { get; set; }
        [Required]
        public string User2Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
