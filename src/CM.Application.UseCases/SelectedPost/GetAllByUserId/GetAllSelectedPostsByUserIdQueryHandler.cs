using CM.Application.Interfaces;
using MediatR;
using FluentValidation;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using Microsoft.Extensions.Logging;
using CM.Application.Dto;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CM.Domain.Entities.PostEntities;

namespace CM.Application.UseCases.SelectedPost.GetAllByUserId
{
    public class GetAllSelectedPostsByUserIdQueryHandler : IRequestHandler<GetAllSelectedPostsByUserIdQuery, BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>>>
    {
        private readonly IValidator<GetAllSelectedPostsByUserIdQuery> _validator;
        private readonly ISelectedPostRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GetAllSelectedPostsByUserIdQueryHandler(
            ILogger<GetAllSelectedPostsByUserIdQueryHandler> logger,
            ISelectedPostRepository repository,
            IValidator<GetAllSelectedPostsByUserIdQuery> validator, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>>> Handle(GetAllSelectedPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);

            if (!validation.IsValid)
            {
                _logger.LogError("Get Post by UserId with UserId {Id} produced errors on validation {Errors}", request.UserId, validation.ToString());
                return new BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>> { Data = null, success = false, Message = "InvalidInput" };
            }
            var items = await _repository.GetAllByCurrentUserIdAsync(request.UserId);

            if (items == null)
            {
                return new BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>> { Data = null, success = false, Message = "NotFound" };
            }
            return new BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>> { Data = items, success = true, Message = "Success" };
        }
    }
}