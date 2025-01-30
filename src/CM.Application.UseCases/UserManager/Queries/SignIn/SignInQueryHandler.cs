using CM.Application.Interfaces;
using MediatR;
using FluentValidation;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using Microsoft.Extensions.Logging;
using CM.Domain.Entities.UserEntities;
using CleanArchitectrure.Application.Dto;

namespace CM.Application.UseCases.UserManager.Queries.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, BaseResponse<(bool, UserLoginDto)>>
    {
        private readonly IValidator<SignInQuery> _validator;
        private readonly IUserManagerRepository _repository;
        private readonly ILogger _logger;

        public SignInQueryHandler(
            ILogger<SignInQueryHandler> logger,
            IUserManagerRepository repository,
            IValidator<SignInQuery> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<BaseResponse<(bool, UserLoginDto)>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);

            if (!validation.IsValid)
            {
                _logger.LogError("Get CheckUserPassQuery by username with {0} produced errors on validation {1}", request.UserName, validation.ToString());
                return new BaseResponse<(bool, UserLoginDto)> { Data = default, success = false, Message = "InvalidInput" };
            }
            var isSignIn = false;
            UserLoginDto userDto = null;
            (isSignIn, userDto) = await _repository.SignIn(request.UserName, request.Password);

            return new BaseResponse<(bool, UserLoginDto)> { Data = (isSignIn, userDto), success = true, Message = "Success" };
        }
    }
}