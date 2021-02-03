using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBlazor.Server.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AuthorId { get; set; }
        public int? CommentId { get; set; }
        public Comment ParentComment { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public DateTimeOffset PostedAt { get; set; }
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }
        public ICollection<Comment> Replies { get; set; }
    }
}
