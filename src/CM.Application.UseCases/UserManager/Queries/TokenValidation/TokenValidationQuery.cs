using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CM.Domain.Entities.UserEntities;
using MediatR;

namespace CM.Application.UseCases.UserManager.Queries.TokenValidation
{
    public class TokenValidationQuery : IRequest<BaseResponse<bool>>
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int UserId{ get; set; }
    }
}