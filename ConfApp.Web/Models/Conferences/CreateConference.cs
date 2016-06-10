namespace ConfApp.Web.Models.Conferences
{
    using System;
    using FluentValidation;

    public class CreateConference
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

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