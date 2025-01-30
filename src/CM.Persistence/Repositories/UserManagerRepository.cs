using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CM.Application.Interfaces;
using CM.Domain.Entities.UserEntities;
using CM.Infrastructure.Helpers;
using CM.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CM.Infrastructure.Repositories
{
    public class UserManagerRepository: IUserManagerRepository
    {
        private CMContext db;
        private IMapper mapper;
        public UserManagerRepository(CMContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<int> RegisterUser(UserInfo userInfo)
        {
            if (userInfo == null)
                return 0;

            userInfo.UserRoles = null;
            userInfo.Sessions = null;

            byte[] salt = null;
            var pass = userInfo.PasswordHash;
            PasswordHelpers.PasswordHasher(out salt, ref pass);
            userInfo.PasswordHash = pass;
            userInfo.Salt = string.Join(",", salt);

            db.UserInfos.Add(userInfo);
            await db.SaveChangesAsync();
            return userInfo.Id;
        }
        public async Task<bool> TokenValidation(string Token,string Role,int userId)
        {
            var session = await db.Sessions.FirstOrDefaultAsync(r => 
                r.UserId == userId &&
                r.Token == Token.ToLower() &&
                r.ExireDateTime >= DateTime.Now &&
                (
                    Role=="" || r.CurrentUserInfo.UserRoles.Any(t=>t.CurrentRole.Title==Role)
                )
            );
            if (session != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> SignOut(string Token)
        {
            var session = await db.Sessions.FirstOrDefaultAsync(r => r.Token == Token);
            if (session != null)
            {
                session.IsDeleted = true;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<(bool, UserLoginDto)> SignIn(string username, string password)
        {
            var IsCorrect = false;
            UserLoginDto userdto = null;
            (IsCorrect, userdto) = await CheckUserPassAsync( username,  password);
            if (IsCorrect)
            {
                var token = Guid.NewGuid().ToString().ToLower();
                db.Sessions.Add(new Session { Token = token, ExireDateTime = DateTime.Now + TimeSpan.FromHours(8), UserId = userdto.UserId });
                await db.SaveChangesAsync();

                var res = await db.UserInfos.Include(r => r.Sessions).Include(r => r.UserRoles).ThenInclude(r => r.CurrentRole).FirstOrDefaultAsync(r => r.UserName.ToLower() == username.ToLower());
                var userDto=mapper.Map<UserLoginDto>(res);
                return (true, userDto);
            }
            return (false,null);
        }
        public async Task<(bool,UserLoginDto)> CheckUserPassAsync(string username, string password)
        {
            var ui = await FindByUsernameAsync(username);
            var isEqual = false;
            UserInfo userInfo = null;
            if (ui != null)
            {
                var hashedPass = PasswordHelpers.PasswordChecker(ui.Salt, password);
                isEqual = hashedPass == ui.PasswordHash;
                if (isEqual)
                    userInfo = await FindByUsernameAsync(username);
            }
            var userDto=mapper.Map<UserLoginDto>(userInfo);
            return (isEqual, userDto);
        }


        public async Task<UserLoginDto> GetAsync(int id)
        {
            var res = await db.UserInfos.Include(r => r.UserRoles).ThenInclude(r => r.CurrentRole).FirstOrDefaultAsync(r => r.Id == id);
            var userDto = mapper.Map<UserLoginDto>(res);

            return userDto;
        }

        public async Task<UserInfo> FindByUsernameAsync(string username)
        {
            var res = await db.UserInfos.Include(r => r.UserRoles).ThenInclude(r => r.CurrentRole).FirstOrDefaultAsync(r=>r.UserName.ToLower()==username.ToLower());
            return res;
        }
    }
}
