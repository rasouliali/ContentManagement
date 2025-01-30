using CleanArchitectrure.Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace CM.UI.Models
{
    public class SignUpDto:UserLoginDto
    {
        [Compare(nameof(Password), ErrorMessage = "Password and PasswordRepeat do not match.")]
        public string PasswordRepeat { get; set; }
    }
}
