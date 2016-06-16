namespace ConfApp.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Domain.Data;
    using Domain.Models;
    using Models.Conferences;
    using System;
    using Domain.Exceptions;

    public class ConferencesController : Controller
    {
        private readonly IConferenceRepository _repository;

        public ConferencesController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var conferences = _repository.Query()
                .Select(c => new ConferenceSummary {Id = c.Id, Name = c.Name})
                .ToList();

            return View(new ConferenceList {Items = conferences});
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateConference model)
        {
            if (ModelState.IsValid)
            {
                var conference = new Conference
                {
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value
                };

                _repository.Save(conference);

                return RedirectToAction("");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            try
            {
                var conference = _repository.FindById(id);

                var model = new EditConference
                {
                    Id = conference.Id,
                    Name = conference.Name,
                    Description = conference.Description,
                    StartDate = conference.StartDate,
                    EndDate = conference.EndDate
                };

                return View(model);
            }
            catch (EntityNotFoundException ex)
            {
                return new HttpNotFoundResult(ex.Message);
            }
        }

        public ActionResult Edit(EditConference model)
        {
            if (ModelState.IsValid)
            {
                var conference = _repository.FindById(model.Id);

                conference.Name = model.Name;
                conference.Description = model.Description;
                conference.StartDate = model.StartDate.Value;
                conference.EndDate = model.EndDate.Value;

                _repository.Save(conference);

                return RedirectToAction("");
            }

            return View(model);
        }
    }
}