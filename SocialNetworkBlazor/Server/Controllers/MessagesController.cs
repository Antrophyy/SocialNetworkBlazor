using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Helpers;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MessagesController(IUnitOfWork uow, ILogger<UsersController> logger, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        // GET: api/Messages
        [HttpGet("{recipientContactId}")]
        public async Task<IActionResult> GetMessages(int recipientContactId)
        {
            var userId = User.Claims.First(x => x.Type == "sub").Value;
            var userContactId = int.Parse(User.Claims.First(x => x.Type == ClaimConstants.USER_CONTACT_ID_CLAIM).Value);

            var recipientUserId = await _uow.UserRepository.SelectData(x => x.Id, x => x.ContactId == recipientContactId);

            var messageList = await _uow.MessageRepository.GetData(x => (x.AuthorID == userId && x.RecipientContactId == recipientContactId)
            || (x.RecipientContactId == userContactId && x.AuthorID == recipientUserId.First()));

            var mappedList = _mapper.Map<List<ClientMessage>>(messageList);
            _logger.LogInformation($"Returned {messageList.Count()} messages.");

            return Ok(mappedList);
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] ClientMessageCreate newMessage)
        {
            newMessage.SentAt = DateTimeOffset.UtcNow;
            var message = _mapper.Map<Message>(newMessage);

            await _uow.MessageRepository.Insert(message);

            var recipientUserId = await _uow.UserRepository.SelectData(x => x.UserName, x => x.ContactId == message.RecipientContactId);

            _logger.LogInformation($"Message with id {message.Id} was created.");
            await _hubContext.Clients.All.SendAsync("message", _mapper.Map<ClientMessage>(message));

            return Ok();
        }
    }
}
