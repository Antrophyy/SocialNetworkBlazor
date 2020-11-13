using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkBlazor.Server.Data;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Shared.Helpers;
using System;
using System.Security.Claims;

[assembly: HostingStartup(typeof(SocialNetworkBlazor.Server.Areas.Identity.IdentityHostingStartup))]
namespace SocialNetworkBlazor.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDatabaseDeveloperPageExceptionFilter();

                services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 4;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;

                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
                .AddDefaultUI()
                .AddSignInManager<AppSingInManager>();

                services.Configure<IdentityOptions>(options =>
                {
                    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                    options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                    options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
                });

                services.AddIdentityServer()
                    .AddApiAuthorization<User, ApplicationDbContext>(options =>
                    {
                        options.IdentityResources["openid"].UserClaims.Add(ClaimConstants.USER_CONTACT_ID_CLAIM);
                    })
                    .AddJwtBearerClientAuthentication();

                services.AddAuthentication()
                    .AddIdentityServerJwt();

                services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
            });
        }
    }
}