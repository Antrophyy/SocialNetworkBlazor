using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork uow, ILogger<UsersController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientUser>>> GetAllUsers()
        {
            var userList = await _uow.UserRepository.GetData().ConfigureAwait(false);
            var mappedList = _mapper.Map<IEnumerable<ClientUser>>(userList);
            _logger.LogInformation($"Returned {userList.Count()} users.");

            return Ok(mappedList);
        }
    }
}

