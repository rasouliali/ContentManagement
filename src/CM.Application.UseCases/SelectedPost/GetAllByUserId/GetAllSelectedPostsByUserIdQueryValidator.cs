using FluentValidation;

namespace CM.Application.UseCases.SelectedPost.GetAllByUserId
{
    public class GetAllSelectedPostsByUserIdQueryValidator : AbstractValidator<GetAllSelectedPostsByUserIdQuery>
    {
        public GetAllSelectedPostsByUserIdQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0);
            //RuleFor(x => x.Id).NotNull();
        }
    }
}