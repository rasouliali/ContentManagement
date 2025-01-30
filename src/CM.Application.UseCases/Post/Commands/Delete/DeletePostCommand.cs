using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CM.Application.UseCases.Post.Commands.Delete
{
    public class DeletePostCommand : IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
    }
}