using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Commands.PostComment.AddEdit;
using CM.Application.Interfaces;
using CM.Application.UseCases.PostComment.Queries.GetAllByPostId;
using CM.Domain.Entities.PostEntities;
using CM.WebApi.Extensions.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostCommentController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IPostCommentRepository _PostCommentRepository;
        private readonly IMediator _mediator;

        public PostCommentController(ILogger<PostCommentController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPostComments")]
        public async Task<BaseResponse<List<PostComment>>> GetPostCommentsAsync([FromQuery] GetPostCommentsQuery input)
        {
            _logger.LogDebug("GetPostCommentsAsync PostId:" + input.PostId);
            var result = await _mediator.Send(input);
            return result;
        }

        [HttpPost(Name = "AddEditPostComment")]
        [BearerAuth]
        public async Task<BaseResponse<int>> AddEditPostComment(AddEditPostCommentCommand item)
        {
            _logger.LogDebug("AddEditPostComment item:" + JsonConvert.SerializeObject(item));
            item.CreatedUserId = SharedLogic.GetUserId(Request);
            var result = await _mediator.Send(item);
            return result;
        }
    }
}
