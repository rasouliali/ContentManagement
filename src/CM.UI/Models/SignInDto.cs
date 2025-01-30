using CleanArchitectrure.Application.Dto;
using CM.Application.UseCases.UserManager.Commands.Register;
using CM.Application.UseCases.UserManager.Queries.SignIn;
using System.ComponentModel.DataAnnotations;

namespace CM.UI.Models
{
    public class SignInDto : SignInQuery
    {
        public bool RememberMe { get; set; }
    }
}
