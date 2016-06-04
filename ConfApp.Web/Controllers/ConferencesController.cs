using System.Linq;
using System.Web.Mvc;
using ConfApp.Web.Data;
using ConfApp.Web.Models.Conferences;

namespace ConfApp.Web.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly IContext _dataContext;

        public ConferencesController(IContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ActionResult Index()
        {
            var conferences = _dataContext
                .Conferences
                .Select(c => new ConferenceSummary {Id = c.Id, Name = c.Name})
                .ToList();

            return View(new ConferenceList { Items = conferences });
        }
    }
}