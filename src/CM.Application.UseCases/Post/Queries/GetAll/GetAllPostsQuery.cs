using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Dto;
using MediatR;

namespace CM.Application.UseCases.Post.Queries.GetAll
{
    public class GetAllPostsQuery : IRequest<BaseResponse<List<PostDto>>>
    {
    }
}