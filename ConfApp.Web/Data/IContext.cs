using System.Data.Entity;
using ConfApp.Web.Data.Models;

namespace ConfApp.Web.Data
{
    public interface IContext
    {
        IDbSet<Conference> Conferences { get; set; }
    }
}