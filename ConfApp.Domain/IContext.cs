namespace ConfApp.Domain
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Models;

    public interface IContext
    {
        IDbSet<Conference> Conferences { get; }
        Task<int> SaveChangesAsync();
    }
}