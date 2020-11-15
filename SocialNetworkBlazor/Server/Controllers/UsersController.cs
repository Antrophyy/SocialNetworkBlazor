using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public async Task<ActionResult<List<ClientUser>>> GetAllUsers()
        {
            var userList = await _uow.UserRepository.GetData().ConfigureAwait(false);
            var mappedList = _mapper.Map<List<ClientUser>>(userList);
            _logger.LogInformation($"Returned {userList.Count()} users.");

            return Ok(mappedList);
        }

        // PUT: api/Users/user
        [HttpPut]
        public async Task<ActionResult<ClientUser>> UpdateUser([FromBody] ClientUserUpdate updatedClientUser)
        {
            var userId = User.Claims.SingleOrDefault(x => x.Type == "sub").Value;

            var user = await _uow.UserRepository.GetById(userId);

            var updatedUser = _mapper.Map(updatedClientUser, user);
            var index = updatedClientUser.ProfileImageTitle.IndexOf('.');
            var newName = updatedClientUser.ProfileImageTitle.Insert(index, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString());
            updatedUser.ProfileImageTitle = newName;
            await _uow.UserRepository.Update(updatedUser);

            CreateNewProfileImageFile(updatedClientUser.ProfileImage, newName);

            return Ok(_mapper.Map<ClientUser>(updatedUser));
        }

        private static void CreateNewProfileImageFile(byte[] byteArray, string filename)
        {
            System.IO.File.WriteAllBytes($"Storage/images/{filename}", byteArray);
        }
    }
}

