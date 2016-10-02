using System.Collections.Generic;
using System.Data;

namespace ConfApp.Tests.Stubs
{
    public class FakeDbConnection : IDbConnection
    {
        public FakeDbConnection()
        {
            ConnectionString = "A fake connection string";
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
        public IDictionary<string, List<object>> Tables { get; set; }
    }
}