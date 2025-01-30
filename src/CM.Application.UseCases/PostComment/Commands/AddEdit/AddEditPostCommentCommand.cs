using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.Commands.PostComment.AddEdit
{
    public class AddEditPostCommentCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int? CommentId { get; set; }
        public int CreatedUserId { get; set; }
    }
}