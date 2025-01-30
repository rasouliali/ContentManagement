using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CM.Application.UseCases.SelectedPost.Add
{
    public class AddSelectedPostCommandHandler : IRequestHandler<AddSelectedPostCommand, BaseResponse<int>>
    {
        private readonly IValidator<AddSelectedPostCommand> _validator;
        private readonly ISelectedPostRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AddSelectedPostCommandHandler(
            ILogger<AddSelectedPostCommandHandler> logger, IMapper mapper,
            ISelectedPostRepository repository,
            IValidator<AddSelectedPostCommand> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<BaseResponse<int>> Handle(AddSelectedPostCommand command, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(command);

            if (!validation.IsValid)
            {
                _logger.LogError("AddSelectedPostCommand with Command with id: {id} produced errors on validation {Errors}", command.PostId, validation.ToString());
                return new BaseResponse<int> { Data = 0, success = false, Message = "InvalidInput" };
            }
            var item = _mapper.Map<Domain.Entities.PostEntities.SelectedPost>(command);
            var id = await _repository.AddEditAsync(item);

            return new BaseResponse<int> { Data = id, success = true, Message = "Success" };
        }
    }
}