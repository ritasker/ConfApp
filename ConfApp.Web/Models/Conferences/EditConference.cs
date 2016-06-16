using System;

namespace ConfApp.Web.Models.Conferences
{
    using FluentValidation;

    public class EditConference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EditConferenceValidator : AbstractValidator<EditConference>
    {
        public EditConferenceValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
        }
    }
}