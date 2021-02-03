using System;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientComment
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public ClientUser Author { get; set; }
        public DateTimeOffset PostedAt { get; set; }
        public string Content { get; set; }
        public List<ClientComment> Replies { get; set; }
    }
}
