using System.Data.Entity;
using ConfApp.Web.Data.Models;

namespace ConfApp.Web.Data
{
    public class ApplicationContext : DbContext, IContext
    {
        public ApplicationContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        public IDbSet<Conference> Conferences { get; set; }
    }
}