using System;

namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientMessage
    {
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public int RecipientContactId { get; set; }

        public DateTimeOffset? SentAt { get; set; }
    }
}
