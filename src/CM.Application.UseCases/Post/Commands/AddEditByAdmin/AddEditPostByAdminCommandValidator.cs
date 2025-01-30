using FluentValidation;

namespace CM.Application.UseCases.Post.Commands.AddEditByAdmin
{
    public class AddEditPostByAdminCommandValidator : AbstractValidator<AddEditPostByAdminCommand>
    {
        public AddEditPostByAdminCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreatedUserId).GreaterThan(0);
            RuleFor(x => x.PostData).NotNull().MinimumLength(1).MaximumLength(4000);
            RuleFor(x => x.ImageFileName).NotNull().MinimumLength(1).MaximumLength(50);
        }
    }
}