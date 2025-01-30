using AutoMapper;
using CM.Application.Dto;
using CM.Application.UseCases.Post.Commands.AddEdit;
using CM.Application.UseCases.Post.Commands.AddEditByAdmin;
using CM.Application.UseCases.SelectedPost.Add;
using CM.Application.UseCases.UserManager.Commands.Register;
using CM.Domain.Entities.PostEntities;

namespace CleanArchitectrure.Application.UseCases.Commons.Mappings
{
    public class SelectedPostProfile : Profile
    {
        public SelectedPostProfile()
        {
            CreateMap<AddSelectedPostCommand, SelectedPost>()
                .ReverseMap();
        }
    }
}
