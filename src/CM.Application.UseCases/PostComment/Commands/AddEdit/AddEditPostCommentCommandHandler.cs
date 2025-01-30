using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CM.Application.Commands.PostComment.AddEdit
{
    public class AddEditPostCommentCommandHandler : IRequestHandler<AddEditPostCommentCommand, BaseResponse<int>>
    {
        private readonly IValidator<AddEditPostCommentCommand> _validator;
        private readonly IPostCommentRepository _PostCommentRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AddEditPostCommentCommandHandler(
            ILogger<AddEditPostCommentCommandHandler> logger, IMapper mapper,
            IPostCommentRepository PostCommentRepository,
            IValidator<AddEditPostCommentCommand> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _PostCommentRepository = PostCommentRepository;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(AddEditPostCommentCommand command, CancellationToken cancellationToken)
        {
            command.CommentId = command.CommentId == 0 ? null : command.CommentId;
            var validation = _validator.Validate(command);

            if (!validation.IsValid)
            {
                _logger.LogError("AddEdit PostComment Command with id: {id} produced errors on validation {Errors}", command.Id, validation.ToString());
                return new BaseResponse<int> { Data = 0, success = false, Message = "InvalidInput" };
            }
            var PostComment = _mapper.Map<CM.Domain.Entities.PostEntities.PostComment>(command);
            var id = await _PostCommentRepository.AddEditAsync(PostComment);

            return new BaseResponse<int> { Data = id, success = true, Message = "NotFound" };
        }
    }
}