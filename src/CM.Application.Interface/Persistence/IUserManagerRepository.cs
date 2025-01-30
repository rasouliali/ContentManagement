
using CleanArchitectrure.Application.Dto;
using CM.Domain.Entities.UserEntities;

namespace CM.Application.Interfaces
{
    public interface IUserManagerRepository
    {
        Task<int> RegisterUser(UserInfo userInfo);
        Task<bool> TokenValidation(string Token, string Role, int userId);
        Task<bool> SignOut(string Token);
        Task<(bool, UserLoginDto)> SignIn(string username, string password);
        Task<(bool, UserLoginDto)> CheckUserPassAsync(string username, string password);
        Task<UserInfo> FindByUsernameAsync(string username);
        Task<UserLoginDto> GetAsync(int Id);
    }
}