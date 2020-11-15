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
        }
    }
}
