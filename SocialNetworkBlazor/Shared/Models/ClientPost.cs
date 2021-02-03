using System;
using System.Collections.Generic;

namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientPost
    {
        public int Id { get; set; }
        public ClientUser Author { get; set; }
        public string Content { get; set; }
        public DateTimeOffset PostedAt { get; set; }
        public List<ClientComment> Comments { get; set; }
    }
}
