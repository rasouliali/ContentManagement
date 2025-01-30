using CM.Application.Interfaces;
using MediatR;
using FluentValidation;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using Microsoft.Extensions.Logging;
using CM.Application.Dto;
using AutoMapper;

namespace CM.Application.UseCases.Post.Queries.GetAll
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, BaseResponse<List<PostDto>>>
    {
        private readonly IValidator<GetAllPostsQuery> _validator;
        private readonly IPostRepository _PostRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(
            ILogger<GetAllPostsQueryHandler> logger,
            IPostRepository PostRepository,
            IValidator<GetAllPostsQuery> validator, IMapper mapper)
        {
            _logger = logger;
            _PostRepository = PostRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<PostDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            //var validation = _validator.Validate(request);

            //if (!validation.IsValid)
            //{
            //    _logger.Error("Get Post by id with id {Id} produced errors on validation {Errors}", request.Id, validation.ToString());
            //    return new QueryResult<List<Domain.PostData.Post>>(result: default(List<Domain.PostData.Post>), type: QueryResultTypeEnum.InvalidInput);
            //}
            var Posts = await _PostRepository.GetAllAsync();

            if (Posts == null)
            {
                return new BaseResponse<List<PostDto>> { Data = null, success = false, Message = "NotFound" };
            }
            var postsDto = _mapper.Map<List<PostDto>>(Posts);
            return new BaseResponse<List<PostDto>> { Data = postsDto, success = true, Message = "Success" };
        }
    }
}