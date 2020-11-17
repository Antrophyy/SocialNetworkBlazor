using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientPostCreate
    {
        [StringLength(1000, ErrorMessage = "Your message doesn't meet the allowed length.", MinimumLength = 1)]
        public string Content { get; set; }
        public string AuthorId { get; set; }
    }
}
