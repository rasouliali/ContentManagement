using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.UseCases.Post.Commands.AddEdit
{
    public class AddEditPostCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string PostData { get; set; }
        public string ImageFileName { get; set; }
        public int CreatedUserId { get; set; }
    }
}