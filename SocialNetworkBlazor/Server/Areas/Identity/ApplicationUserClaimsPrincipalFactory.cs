using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Shared.Helpers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Areas.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options
            ) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity>
            GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimConstants.USER_CONTACT_ID_CLAIM, $"{user.ContactId}"),
            });

            return identity;
        }
    }
}