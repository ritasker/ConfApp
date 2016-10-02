using System.Linq;
using ConfApp.Tests.Stubs;
using Dapper;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests
{
    public class FakeDbConnectionTests
    {
        [Fact]
        public void should_insert_data()
        {
            using (var conn = new FakeDbConnection())
            {
                var result = conn.Execute("INSERT INTO myTable (Id, Text) VALUES(@Id, @Text)", new { Id = 1, Text = "Qwerty" });

                //ASERT
                result.Should().Be(1);
                var table = conn.Tables.First();
                table.Key.Should().Be("myTable");
                table.Value.Count.Should().Be(1);
                var row = table.Value.First();
                row.Should().Be(new {Id = 1, Text = "Qwerty"});
            }

        }

        // should update data
        // should query data
        // should delete data
        // should insert data in a transaction
        // should update data in a transaction
        // should query data in a transaction
        // should delete data in a transaction
        // should not insert data when a transaction fails
        // should not update data when a transaction fails
        // should not delete data when a transaction fails
    }
}