using System;
using System.Collections.Generic;
using System.Data;

namespace ConfApp.Tests.Stubs
{
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
           // Connection.
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public IDbConnection Connection { get; }
        public IsolationLevel IsolationLevel { get; }
    }
}