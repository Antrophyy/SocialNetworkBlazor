using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialNetworkBlazor.Server.Models;

namespace SocialNetworkBlazor.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasKey(p => p.Id);

            builder.Entity<Message>()
                .Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Entity<User>()
                .Property(b => b.ProfileImageTitle)
                .HasDefaultValue("no_profile_image.png");

            builder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<Comment>()
                .HasMany(p => p.Replies)
                .WithOne(p => p.ParentComment)
                .HasForeignKey(p => p.CommentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Friendship>()
                .HasKey(f => new { f.User1Id, f.User2Id });

            builder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany()
                .HasForeignKey(f => f.User1Id);

            builder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
