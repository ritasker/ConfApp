namespace ConfApp.Data
{
    using System.Data.Entity;
    using Domain;
    using Domain.Models;

    public class ApplicationContext : DbContext, IContext
    {
        public ApplicationContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        public IDbSet<ConferenceDetails> Conferences { get; set; }
        public IDbSet<ConferenceSummary> ConferenceSummaries { get; set; }
    }
}