using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Application.Dto;
using MediatR;

namespace CM.Application.UseCases.SelectedPost.GetAllByUserId
{
    public class GetAllSelectedPostsByUserIdQuery : IRequest<BaseResponse<List<Domain.Entities.PostEntities.SelectedPost>>>
    {
        public int UserId { get; set; }
    }
}