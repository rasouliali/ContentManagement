using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.UseCases.PostComment.Queries.GetAllByPostId
{
    public class GetPostCommentsQuery : IRequest<BaseResponse<List<Domain.Entities.PostEntities.PostComment>>>
    {
        public int PostId { get; set; }
    }
}