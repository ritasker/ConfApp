using System;
using System.Data;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Infrastructure;

namespace ConfApp.Domain.Conferences.Handlers
{
    public class CreateConfereceHandler : ICommandHandler<CreateConference, Guid>
    {
        private readonly IDbConnection _connection;

        public CreateConfereceHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public Guid Handle(CreateConference command)
        {
            var id = Guid.NewGuid();
            var createConference = new SqlCommands.CreateConference(id, command.Name, command.Description, command.StartDate.Value, command.EndDate.Value);
            createConference.Execute(_connection);

            return id;
        }
    }
}