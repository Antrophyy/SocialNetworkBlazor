using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialNetworkBlazor.Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Areas.Identity
{
    public class AppSingInManager : SignInManager<User>
    {
        public AppSingInManager(UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task SignInAsync(User user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            await base.SignInAsync(user, authenticationProperties, authenticationMethod);

            user.LastLoginDate = DateTimeOffset.UtcNow;
            var updateResult = await UserManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errorList = updateResult.Errors.Select(x => $"{x.Code}: {x.Description}");
                throw new Exception("Failed to update applicationUser last login: " + string.Join("; ", errorList));
            }
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(userName);
                user.LastLoginDate = DateTimeOffset.UtcNow;
                await UserManager.UpdateAsync(user);
            }
            return result;
        }
    }
}