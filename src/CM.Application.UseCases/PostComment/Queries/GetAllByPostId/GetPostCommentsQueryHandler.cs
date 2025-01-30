using CM.Application.Interfaces;
using MediatR;
using FluentValidation;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using Microsoft.Extensions.Logging;

namespace CM.Application.UseCases.PostComment.Queries.GetAllByPostId
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, BaseResponse<List<Domain.Entities.PostEntities.PostComment>>>
    {
        private readonly IValidator<GetPostCommentsQuery> _validator;
        private readonly IPostCommentRepository _CommentRepository;
        private readonly ILogger _logger;

        public GetPostCommentsQueryHandler(
            ILogger<GetPostCommentsQueryHandler> logger,
            IPostCommentRepository CommentRepository,
            IValidator<GetPostCommentsQuery> validator)
        {
            _logger = logger;
            _CommentRepository = CommentRepository;
            _validator = validator;
        }

        public async Task<BaseResponse<List<Domain.Entities.PostEntities.PostComment>>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);

            if (!validation.IsValid)
            {
                _logger.LogError("Get PostComment by postId with {0} produced errors on validation {1}", request.PostId, validation.ToString());
                return new BaseResponse<List<Domain.Entities.PostEntities.PostComment>> { Data = default(List<Domain.Entities.PostEntities.PostComment>), success = false, Message = "InvalidInput" };
            }
            var Comment = await _CommentRepository.GetByPostIdAsync(request.PostId);

            if (Comment == null)
            {
                return new BaseResponse<List<Domain.Entities.PostEntities.PostComment>> { Data = Comment, success = false, Message = "NotFound"};
            }
            return new BaseResponse<List<Domain.Entities.PostEntities.PostComment>> { Data = Comment, success = true, Message = "Success" };
        }
    }
}