namespace SocialNetworkBlazor.Shared.Models
{
    public class ClientCommentCreate
    {
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
    }
}
