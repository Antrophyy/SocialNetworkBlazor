using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service.Repository;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Service
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        void Dispose();
    }
}
