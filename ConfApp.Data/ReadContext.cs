namespace ConfApp.Data
{
    using System.Data.Entity;
    using Domain;
    using Domain.Models;

    public class ReadContext : DbContext, IReadContext
    {
        public ReadContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ReadContext>());
        }

        public IDbSet<ConferenceDetails> Conferences { get; set; }
        public IDbSet<ConferenceSummary> ConferenceSummaries { get; set; }
    }
}