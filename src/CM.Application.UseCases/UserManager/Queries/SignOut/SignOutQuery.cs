using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Domain.Entities.UserEntities;
using MediatR;

namespace CM.Application.UseCases.UserManager.Queries.SignOut
{
    public class SignOutQuery : IRequest<BaseResponse<bool>>
    {
        public string Token { get; set; }
    }
}