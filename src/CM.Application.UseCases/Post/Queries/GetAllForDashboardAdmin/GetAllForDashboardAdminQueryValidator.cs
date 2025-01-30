using FluentValidation;

namespace CM.Application.UseCases.Post.Queries.GetAllForDashboardAdmin
{
    public class GetAllForDashboardAdminQueryValidator : AbstractValidator<GetAllForDashboardAdminQuery>
    {
        public GetAllForDashboardAdminQueryValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0);
            //RuleFor(x => x.Id).NotNull();
        }
    }
}