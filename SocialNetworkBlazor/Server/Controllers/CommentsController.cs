using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CommentsController> _logger;
        private readonly IMapper _mapper;

        public CommentsController(IUnitOfWork uow, ILogger<CommentsController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] ClientCommentCreate newComment)
        {
            var comment = _mapper.Map<Comment>(newComment);
            comment.PostedAt = DateTimeOffset.UtcNow;

            await _uow.CommentRepository.Insert(comment);

            _logger.LogInformation($"Comment with id {comment.Id} was created.");
            var returnComment = _mapper.Map<ClientComment>(comment);
            var userAuthor = await _uow.UserRepository.GetById(newComment.AuthorId);
            returnComment.Author = _mapper.Map<ClientUser>(userAuthor);
            return Ok(returnComment);
        }
    }
}
