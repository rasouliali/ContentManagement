using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CM.Application.UseCases.Post.Commands.AddEdit;
using CM.Application.UseCases.UserManager.Commands.Register;
using CM.Domain.Entities.PostEntities;
using CM.Domain.Entities.UserEntities;

namespace CleanArchitectrure.Application.UseCases.Commons.Mappings
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfo, UserLoginDto>().AfterMap((s, d) =>
            {
                d.UserId = s.Id;
                if (s.Sessions != null)
                {
                    d.Token = s.Sessions.Where(r=>r.IsDeleted==false).OrderByDescending(r => r.ExireDateTime).Select(r => r.Token).FirstOrDefault();
                    d.ExpireDateTime = s.Sessions.Where(r => r.IsDeleted == false).OrderByDescending(r => r.ExireDateTime).Select(r => r.ExireDateTime).FirstOrDefault();
                }
                if (s.UserRoles != null)
                    d.Roles = string.Join(",", s.UserRoles.Select(r => r.CurrentRole.Title).ToList());
                else
                    d.Roles = "";

            });
            CreateMap<UserLoginDto, UserInfo>().AfterMap((s, d) =>
            {
                d.Id = s.UserId;
                d.PasswordHash = s.Password;
            });
            CreateMap<RegisterCommand, UserInfo>().AfterMap((s, d) =>
            {
                d.PasswordHash = s.Password;
            });
        }
    }
}
