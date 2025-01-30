using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Interfaces;
using CM.Application.UseCases.SelectedPost.Add;
using CM.Application.UseCases.SelectedPost.GetAllByUserId;
using CM.Domain.Entities.PostEntities;
using CM.WebApi.Extensions.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SelectedPostController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public SelectedPostController(ILogger<SelectedPostController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllSelectedPosts")]
        public async Task<BaseResponse<List<SelectedPost>>> GetSelectedPostsAsync()
        {
            GetAllSelectedPostsByUserIdQuery input = new GetAllSelectedPostsByUserIdQuery();
            input.UserId = SharedLogic.GetUserId(Request);

            _logger.LogDebug("GetSelectedPostsAsync PostId:" + input.UserId);
            var result = await _mediator.Send(input);
            return result;
        }

        [HttpPost(Name = "AddEditSelectedPost")]
        [BearerAuth]
        public async Task<BaseResponse<int>> AddEditSelectedPost(AddSelectedPostCommand item)
        {
            _logger.LogDebug("AddEditSelectedPost item:" + JsonConvert.SerializeObject(item));
            item.CreatedUserId = SharedLogic.GetUserId(Request);
            var result = await _mediator.Send(item);
            return result;
        }
    }
}
