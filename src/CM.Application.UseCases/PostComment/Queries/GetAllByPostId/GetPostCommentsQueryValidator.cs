using FluentValidation;

namespace CM.Application.UseCases.PostComment.Queries.GetAllByPostId
{
    public class GetPostCommentsQueryValidator : AbstractValidator<GetPostCommentsQuery>
    {
        public GetPostCommentsQueryValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.PostId).NotNull();
        }
    }
}