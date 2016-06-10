using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ConfApp.Web.Models.Conferences;

namespace ConfApp.Web.Controllers
{
    using Domain;
    using Domain.Models;

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateConference model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var conference = new Conference
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate.Value,
                EndDate = model.EndDate.Value
            };

            _dataContext.Conferences.Add(conference);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("");
        }

    }
}