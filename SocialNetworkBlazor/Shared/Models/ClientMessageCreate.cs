using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientMessageCreate
    {
        
        [StringLength(1000, ErrorMessage = "Your message doesn't meet the allowed length.", MinimumLength = 1)]
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public int RecipientContactId { get; set; }

        public DateTimeOffset? SentAt { get; set; }
    }
}
