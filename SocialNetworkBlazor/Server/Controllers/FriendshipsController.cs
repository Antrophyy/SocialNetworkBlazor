using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<FriendshipsController> _logger;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FriendshipsController(IUnitOfWork uow, ILogger<FriendshipsController> logger, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        //GET:api/Friendships/Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            var relationship = await _uow.FriendshipRepository.GetData(x => x.User2Id == id || x.User1Id == id, includeProperties: "User1, User2");
            var mappedList = _mapper.Map<List<ClientFriendship>>(relationship);
            _logger.LogInformation($"Returned {mappedList.Count} relationships");
            return Ok(mappedList);
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest([FromBody] ClientFriendshipCreate clientFriendship)
        {
            var friendship = _mapper.Map<Friendship>(clientFriendship);
            friendship.User1Id = clientFriendship.User1.Id;
            friendship.User2Id = clientFriendship.User2.Id;
            friendship.User2 = null;
            friendship.User1 = null;
            await _uow.FriendshipRepository.Insert(friendship);
            _logger.LogInformation("New friend request inserted");

            var friendshipToSend = _mapper.Map<ClientFriendship>(clientFriendship);
            friendshipToSend.User1 = clientFriendship.User1;
            friendshipToSend.User2 = clientFriendship.User2;
            await _hubContext.Clients.All.SendAsync("friendRequest", _mapper.Map<ClientFriendship>(friendshipToSend));

            return Ok();
        }

        [HttpDelete("{user1Id}/{user2Id}")]
        public async Task<IActionResult> DeleteFriendRequest(string user1Id, string user2Id)
        {
            var foundFriendship = await _uow.FriendshipRepository.GetData(x => (x.User1Id == user1Id && x.User2Id == user2Id)
            || (x.User1Id == user2Id && x.User2Id == user1Id));

            await _uow.FriendshipRepository.Delete(foundFriendship.Single());
            _logger.LogInformation("Friendship deleted");
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFriendship([FromBody] ClientFriendshipUpdate clientFriendship)
        {
            var query = await _uow.FriendshipRepository
                .GetData(x => x.User1Id == clientFriendship.User1.Id && x.User2Id == clientFriendship.User2.Id);

            var foundFriendship = query.Single();
            var mappedFriendship = _mapper.Map(clientFriendship, foundFriendship);
            
            mappedFriendship.User1 = null;
            mappedFriendship.User2 = null;

            await _uow.FriendshipRepository.Update(mappedFriendship);
            _logger.LogInformation("Friendship accepted");

            return Ok(_mapper.Map<ClientFriendship>(clientFriendship));
        }
    }
}
