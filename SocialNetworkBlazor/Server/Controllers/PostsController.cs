using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkBlazor.Server.Models;
using SocialNetworkBlazor.Server.Service;
using SocialNetworkBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<PostsController> _logger;
        private readonly IMapper _mapper;

        public PostsController(IUnitOfWork uow, ILogger<PostsController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<List<ClientPost>>> GetAllPosts()
        {
            var postsList = await _uow.PostRepository.GetData(includeProperties: "Author, Comments",
                orderBy: new Func<IQueryable<Post>, IOrderedQueryable<Post>>(x=>x.OrderBy(x=>x.PostedAt)));

            foreach (var post in postsList)
            {
                foreach (var comment in post.Comments)
                {
                    var replies = await _uow.CommentRepository.GetData(x => x.CommentId == comment.Id,
                        orderBy: new Func<IQueryable<Comment>, IOrderedQueryable<Comment>>(x => x.OrderBy(x => x.PostedAt)));

                    comment.Replies = replies.ToList();
                }
            }
            var mappedList = _mapper.Map<List<ClientPost>>(postsList);
            _logger.LogInformation($"Returned {mappedList.Count} posts.");

            return Ok(mappedList);
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] ClientPostCreate newPost)
        {
            var post = _mapper.Map<Post>(newPost);
            post.PostedAt = DateTimeOffset.UtcNow;

            await _uow.PostRepository.Insert(post);

            _logger.LogInformation($"Post with id {post.Id} was created.");
            var returnPost = _mapper.Map<ClientPost>(post);
            var userAuthor = await _uow.UserRepository.GetById(newPost.AuthorId);
            returnPost.Author = _mapper.Map<ClientUser>(userAuthor);
            return Ok(returnPost);
        }
    }
}
