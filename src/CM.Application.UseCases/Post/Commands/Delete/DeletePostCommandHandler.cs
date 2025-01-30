using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CM.Application.UseCases.Post.Commands.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, BaseResponse<bool>>
    {
        private readonly IValidator<DeletePostCommand> _validator;
        private readonly IPostRepository _PostRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DeletePostCommandHandler(
            ILogger<DeletePostCommandHandler> logger, IMapper mapper,
            IPostRepository PostRepository,
            IValidator<DeletePostCommand> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _PostRepository = PostRepository;
            _validator = validator;
        }

        public async Task<BaseResponse<bool>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(command);

            if (!validation.IsValid)
            {
                _logger.LogError("Update Post Name Command with id: {id} produced errors on validation {Errors}", command.Id, validation.ToString());
                return new BaseResponse<bool> { Data = false, success = false, Message = "InvalidInput" };
            }
            var res = await _PostRepository.DeleteAsync(command.Id);

            return new BaseResponse<bool> { Data = res, success = true, Message = "Success" };
        }
    }
}