using FluentValidation;

namespace CM.Application.UseCases.Post.Commands.Delete
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}