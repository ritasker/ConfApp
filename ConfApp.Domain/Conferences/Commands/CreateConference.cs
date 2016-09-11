using System;
using ConfApp.Domain.Infrastructure;

namespace ConfApp.Domain.Conferences.Commands
{
    public class CreateConference : ICommand<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}