using System;
using System.Collections.Generic;
using System.Data;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Conferences.Handlers;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests.Handlers
{
    public class CreateConferenceHandlerTests
    {
        [Fact]
        public void ShouldReturnTheIdOfTheNewConference()
        {
            // ARRANGE
            var createConference = new CreateConference
            {
                Name = string.Join(" ", Faker.Lorem.Words(3)),
                Description = string.Join(" ", Faker.Lorem.Sentence()),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var subject = new CreateConfereceHandler(new FakeDbConnection());

            // ACT
            Guid result = subject.Handle(createConference);

            // ASSERT
            result.Should().NotBeEmpty();
        }
    }

    public class FakeDbConnection : IDbConnection
    {
        public FakeDbConnection(string connectionString)
        {
            ConnectionString = connectionString;
            ConnectionTimeout = 100;
        }

        public void Dispose()
        {   
        }

        public IDbTransaction BeginTransaction()
        {
            return new FakeDbTransaction(this, IsolationLevel.Unspecified);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new FakeDbTransaction(this, il);
        }

        public void Close()
        {
            State = ConnectionState.Closed;
        }

        public void ChangeDatabase(string databaseName)
        {
            Database = databaseName;
        }

        public IDbCommand CreateCommand()
        {
            return new FakeDbCommand(this);
        }

        public void Open()
        {
            State = ConnectionState.Open;
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; }
        public string Database { get; private set; }
        public ConnectionState State { get; private set; }
    }

    public class FakeDbTransaction : IDbTransaction
    {
        private List<object> _data;

        public FakeDbTransaction(FakeDbConnection connection, IsolationLevel isolationLevel)
        {
            Connection = connection;
            IsolationLevel = isolationLevel;
            _data = new List<object>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            Connection.
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public IDbConnection Connection { get; }
        public IsolationLevel IsolationLevel { get; }
    }

    public class FakeDbCommand : IDbCommand
    {
        public FakeDbCommand(FakeDbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDataParameterCollection Parameters { get; }
        public UpdateRowSource UpdatedRowSource { get; set; }
    }
}