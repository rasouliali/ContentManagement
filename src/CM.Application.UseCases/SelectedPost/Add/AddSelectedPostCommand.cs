using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.UseCases.SelectedPost.Add
{
    public class AddSelectedPostCommand : IRequest<BaseResponse<int>>
    {
        public int CreatedUserId { get; set; }
        public int PostId { get; set; }
    }
}