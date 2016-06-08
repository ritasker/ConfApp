using System.Data.Entity;
using System.Threading.Tasks;
using ConfApp.Web.Data.Models;

namespace ConfApp.Web.Data
{
    public interface IContext
    {
        IDbSet<Conference> Conferences { get; }
        Task<int> SaveChangesAsync();
    }
}