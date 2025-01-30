using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Domain.Entities.UserEntities;
using MediatR;

namespace CM.Application.UseCases.UserManager.Queries.SignIn
{
    public class SignInQuery : IRequest<BaseResponse<(bool, UserLoginDto)>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}