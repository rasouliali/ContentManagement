
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Dto;
using CM.Application.UseCases.Post.Commands.AddEdit;
using CM.Application.UseCases.Post.Commands.AddEditByAdmin;
using CM.Application.UseCases.Post.Commands.Delete;
using CM.Application.UseCases.Post.Queries.GetAll;
using CM.Application.UseCases.Post.Queries.GetAllForDashboardAdmin;
using CM.Application.UseCases.UserManager.Commands.Register;
using CM.Domain.Entities.PostEntities;
using CM.Persistence.Contexts;
using CM.WebApi.Extensions.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;
        private readonly IMediator _mediator;

        public PostController(ILogger<PostController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetPosts")]
        public async Task<BaseResponse<List<PostDto>>> GetPostsAsync()
        {
            GetAllPostsQuery input = new GetAllPostsQuery();
            _logger.LogDebug("GetPostsAsync");
            var result = await _mediator.Send(input);
            return result;
        }

        [BearerAuth("Admin")]
        [HttpGet("GetPostsForDashboardAdmin",Name = "GetPostsForDashboardAdmin")]
        public async Task<BaseResponse<List<PostDto>>> GetPostsForDashboardAdminAsync()
        {
            var input = new GetAllForDashboardAdminQuery();
            input.UserId = SharedLogic.GetUserId(Request);
            _logger.LogDebug("GetPostsAsync");
            var result = await _mediator.Send(input);
            return result;
        }
        [BearerAuth]
        [HttpPost(Name = "AddEditPost")]
        public async Task<BaseResponse<int>> AddEditPost(AddEditPostCommand item)
        {
            _logger.LogDebug("AddEditPostComment item:" + JsonConvert.SerializeObject(item));
            item.CreatedUserId = SharedLogic.GetUserId(Request);
            var result = await _mediator.Send(item);
            return result;
        }

        [BearerAuth("Admin")]
        [HttpPost("AddEditPostByAdmin", Name = "AddEditPostByAdmin")]
        public async Task<BaseResponse<int>> AddEditPostByAdmin(AddEditPostByAdminCommand item)
        {
            _logger.LogDebug("AddEditPostByAdmin item:" + JsonConvert.SerializeObject(item));
            item.CreatedUserId = SharedLogic.GetUserId(Request);
            var result = await _mediator.Send(item);
            return result;
        }
        [HttpDelete(Name = "DeletePost")]
        public async Task<BaseResponse<bool>>  DeletePost(DeletePostCommand input)
        {
            _logger.LogDebug("DeletePost Id:" + input.Id);
            var result = await _mediator.Send(input);
            return result;
        }
    }
}
