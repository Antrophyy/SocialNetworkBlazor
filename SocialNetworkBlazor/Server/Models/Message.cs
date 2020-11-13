using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Server.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Content { get; set; }

        [Required]
        public string AuthorID { get; set; }

        [Required]
        public int RecipientContactId { get; set; }

        [Required]
        public DateTimeOffset? SentAt { get; set; }
    }
}
