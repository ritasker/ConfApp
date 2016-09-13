using System.Data;

namespace ConfApp.Domain.Data
{
    public interface ISqlCommand
    {
        void Execute(IDbConnection connection);
    }
}