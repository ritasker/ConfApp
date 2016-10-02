using System;
using System.Data;
using System.Data.SqlClient;
using ConfApp.Domain.Data;
using Dapper;

namespace ConfApp.Domain.Conferences.SqlCommands
{
    public class CreateConference : ISqlCommand
    {
        public CreateConference(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            _id = id;
            _name = name;
            _description = description;
            _startDate = startDate;
            _endDate = endDate;

            _sql = @"INSERT INTO Conferences (id, name, description, startdate, enddate) 
                        VALUES(@Id, @Name, @Description, @StartDate, @EndDate)";
        }

        public void Execute(IDbConnection connection)
        {
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                connection.Execute(_sql, new
                {
                    Id = _id,
                    Name = _name,
                    Description = _description,
                    StartDate = _startDate,
                    EndDate = _endDate
                }, transaction);
                transaction.Commit();

            }
            catch (SqlException)
            {
                transaction.Rollback();
            }

            
        }

        private readonly string _sql;
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _description;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
    }
}