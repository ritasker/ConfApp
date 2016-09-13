namespace ConfApp.Domain
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Models;

    public interface IReadContext
    {
        IDbSet<ConferenceDetails> Conferences { get; }
        IDbSet<ConferenceSummary> ConferenceSummaries { get; }
        Task<int> SaveChangesAsync();
    }
}