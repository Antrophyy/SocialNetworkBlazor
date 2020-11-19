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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllPosts(string id)
        {
            var queryList = await _uow.PostRepository.GetWithRawSql("SELECT p.Id,p.AuthorId,p.Content,p.PostedAt FROM Friendship f"
                + " INNER JOIN AspNetUsers u"
                + " ON f.User1Id = u.Id OR f.User2Id = u.Id"
                + " INNER JOIN Post p"
                + " ON f.User1Id = p.AuthorId OR f.User2Id = p.AuthorId"
                + $" WHERE (f.User1Id = p.AuthorId AND f.User2Id = '{id}')"
                + $" OR (f.User2Id = p.AuthorId AND f.User1Id = '{id}')"
                );

            foreach (var post in queryList)
            {
                var comments = await _uow.CommentRepository.GetData(x => x.PostId == post.Id);
                post.Comments = comments.ToList();

                var authorList = await _uow.UserRepository.GetData(x => x.Id == post.AuthorId);
                post.Author = authorList.Single();

                foreach (var comment in post.Comments)
                {
                    var replies = await _uow.CommentRepository.GetData(x => x.CommentId == comment.Id,
                        orderBy: new Func<IQueryable<Comment>, IOrderedQueryable<Comment>>(x => x.OrderBy(x => x.PostedAt)));

                    comment.Replies = replies.ToList();
                }
            }
            var mappedList = _mapper.Map<List<ClientPost>>(queryList);
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
