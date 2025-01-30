using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.UseCases.Post.Commands.AddEditByAdmin
{
    public class AddEditPostByAdminCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string PostData { get; set; }
        public string ImageFileName { get; set; }
        public int CreatedUserId { get; set; }
        public int? Capacity { get; set; }
        public DateTime? TutorialDate { get; set; }
    }
}