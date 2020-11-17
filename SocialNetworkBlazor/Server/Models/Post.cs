using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Server.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }
        [Required]
        public DateTimeOffset PostedAt { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
