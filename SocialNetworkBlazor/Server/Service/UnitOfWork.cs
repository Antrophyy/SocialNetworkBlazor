using SocialNetworkBlazor.Server.Data;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service.Repository;
using System;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Service
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IRepository<User> _userRepository;
        private readonly ApplicationDbContext _context;
        private bool disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new Repository<User>(_context);
                return _userRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
