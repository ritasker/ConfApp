using ConfApp.Domain.Conferences.Commands;
using FluentValidation;

namespace ConfApp.Web.Models.Conferences
{
    public class CreateConferenceValidator : AbstractValidator<CreateConference>
    {
        public CreateConferenceValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
        }
    }
}