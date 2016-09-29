using System;
using System.Data.SqlClient;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Infrastructure;

namespace ConfApp.Domain.Conferences.Handlers
{
    public class CreateConfereceHandler : ICommandHandler<CreateConference, Guid>
    {
        public Guid Handle(CreateConference command)
        {
            var id = Guid.NewGuid();
            using (var connection =
                    new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFileName=|DataDirectory|\ConfApp.Data.ApplicationContext.mdf;Integrated Security=True;"))
            {
                var createConference = new SqlCommands.CreateConference(id, command.Name, command.Description, command.StartDate.Value, command.EndDate.Value);
                createConference.Execute(connection);
            }

            return id;
        }
    }
}