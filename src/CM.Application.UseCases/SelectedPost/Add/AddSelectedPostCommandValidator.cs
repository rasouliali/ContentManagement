using FluentValidation;

namespace CM.Application.UseCases.SelectedPost.Add
{
    public class AddSelectedPostCommandValidator : AbstractValidator<AddSelectedPostCommand>
    {
        public AddSelectedPostCommandValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.CreatedUserId).GreaterThan(0);
        }
    }
}