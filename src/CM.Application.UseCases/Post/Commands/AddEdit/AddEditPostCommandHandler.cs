using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CM.Application.UseCases.Post.Commands.AddEdit
{
    public class AddEditPostCommandHandler : IRequestHandler<AddEditPostCommand, BaseResponse<int>>
    {
        private readonly IValidator<AddEditPostCommand> _validator;
        private readonly IPostRepository _PostRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AddEditPostCommandHandler(
            ILogger<AddEditPostCommandHandler> logger, IMapper mapper,
            IPostRepository PostRepository,
            IValidator<AddEditPostCommand> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _PostRepository = PostRepository;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(AddEditPostCommand command, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(command);

            if (!validation.IsValid)
            {
                _logger.LogError("Update Post Name Command with id: {id} produced errors on validation {Errors}", command.Id, validation.ToString());
                return new BaseResponse<int> { Data = 0, success = false, Message = "InvalidInput" };
            }
            var post = _mapper.Map<Domain.Entities.PostEntities.Post>(command);
            var id = await _PostRepository.AddEditAsync(post);

            return new BaseResponse<int> { Data = id, success = true, Message = "Success" };
        }
    }
}