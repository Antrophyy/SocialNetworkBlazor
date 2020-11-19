using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetworkBlazor.Server.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        public int ContactId { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        [Required]
        public bool IsOnline { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProfileImageTitle { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        [NotMapped]
        public ICollection<Friendship> Friendships { get; set; }
    }
}
