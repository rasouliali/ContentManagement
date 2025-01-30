using FluentValidation;

namespace CM.Application.Commands.PostComment.AddEdit
{
    public class AddEditPostCommentCommandValidator : AbstractValidator<AddEditPostCommentCommand>
    {
        public AddEditPostCommentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreatedUserId).NotNull().GreaterThan(0);
            RuleFor(x => x.PostId).NotNull().GreaterThan(0);
            RuleFor(x => x.Comment).NotNull().MinimumLength(1).MaximumLength(4000);
        }
    }
}