using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service.Repository;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Service
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Message> MessageRepository { get; }
        IRepository<Post> PostRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        void Dispose();
    }
}
