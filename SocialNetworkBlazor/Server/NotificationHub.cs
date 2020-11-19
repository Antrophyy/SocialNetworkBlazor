using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server
{
    public class NotificationHub : Hub
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(IUnitOfWork uow, ILogger<NotificationHub> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await SetIsOnlineProperty(false);

            await base.OnDisconnectedAsync(exception);
        }

        public override async Task OnConnectedAsync()
        {
            await SetIsOnlineProperty(true);

            await base.OnConnectedAsync();
        }
        private async Task SetIsOnlineProperty(bool isOnline)
        {
            Claim idClaim = Context.User.Claims.SingleOrDefault(x => x.Type == "sub");

            if (idClaim == null || !(idClaim.Value is string))
                return;

            string userId = idClaim.Value;

            var user = await _uow.UserRepository.GetById(userId);

            user.IsOnline = isOnline;

            await _uow.UserRepository.Update(user);

            if (isOnline)
                _logger.LogInformation($"User with id {user.Id} is online.");
            else
                _logger.LogInformation($"User with id {user.Id} is offline.");

            await Clients.All.SendAsync("statusChange", new StatusChange { ContactId = user.ContactId, IsOnline = user.IsOnline });
        }
    }
}
