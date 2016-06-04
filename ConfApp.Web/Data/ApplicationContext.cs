using System.Data.Entity;
using ConfApp.Web.Data.Models;

namespace ConfApp.Web.Data
{
    public class ApplicationContext : DbContext, IContext
    {
        public IDbSet<Conference> Conferences { get; set; }
    }
}