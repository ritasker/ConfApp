using System;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Infrastructure;

namespace ConfApp.Domain.Conferences.Handlers
{
    public class CreateConfereceHandler : CommandHandler<CreateConference, Guid>
    {
        public override Guid Handle(ICommand<Guid> message)
        {
            return Guid.NewGuid();
        }
    }
}