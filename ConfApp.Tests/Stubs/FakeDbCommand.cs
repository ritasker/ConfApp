using System;
using System.Data;

namespace ConfApp.Tests.Stubs
{
    public class FakeDbCommand : IDbCommand
    {
        public FakeDbCommand(FakeDbConnection connection)
        {
            Connection = connection;
        }

        public void Dispose()
        {
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
            if (Parameters == null)
            {
                Parameters = new DataPar
            }
            return new InMemoryDataParameter();
        }

        public int ExecuteNonQuery()
        {
            return 1;
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

    public class InMemoryDataParameter : IDbDataParameter
    {
        public DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }
        public bool IsNullable { get; }
        public string ParameterName { get; set; }
        public string SourceColumn { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public object Value { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }
        public int Size { get; set; }
    }
}