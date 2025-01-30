using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Dto;
using MediatR;

namespace CM.Application.UseCases.Post.Queries.GetAllForDashboardAdmin
{
    public class GetAllForDashboardAdminQuery : IRequest<BaseResponse<List<PostDto>>>
    {
        public int UserId { get; set; }
    }
}