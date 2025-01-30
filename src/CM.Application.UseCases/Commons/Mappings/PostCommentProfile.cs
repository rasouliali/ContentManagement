using AutoMapper;
using CM.Application.Commands.PostComment.AddEdit;
using CM.Application.Dto;
using CM.Domain.Entities.PostEntities;

namespace CleanArchitectrure.Application.UseCases.Commons.Mappings
{
    public class PostCommentProfile : Profile
    {
        public PostCommentProfile()
        {
            CreateMap<PostComment, AddEditPostCommentCommand>()
                .ReverseMap();
            CreateMap<PostComment, PostCommentDto>().AfterMap((s, d) =>
            {
                if(s.CurrentCreatedUser!=null)
                    d.CurrentUserFullname = s.CurrentCreatedUser.FullName;
            });
            CreateMap<PostCommentDto, PostComment>();
        }
    }
}
