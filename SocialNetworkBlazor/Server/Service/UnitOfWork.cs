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
        private IRepository<Message> _messageRepository;
        private IRepository<Post> _postRepository;
        private IRepository<Comment> _commentRepository;
        private IRepository<Friendship> _friendshipRepository;

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
        
        public IRepository<Message> MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new Repository<Message>(_context);
                return _messageRepository;
            }
        }

        public IRepository<Post> PostRepository
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new Repository<Post>(_context);
                return _postRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new Repository<Comment>(_context);
                return _commentRepository;
            }
        }
        public IRepository<Friendship> FriendshipRepository
        {
            get
            {
                if (_friendshipRepository == null)
                    _friendshipRepository = new Repository<Friendship>(_context);
                return _friendshipRepository;
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
