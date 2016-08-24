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

        [HttpGet]
        public ActionResult Index(int top = 10, int skip = 0)
        {
            var conferences = _repository.FindAll(top, skip);

            return View(conferences);
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
                var conference = new ConferenceDetails
                {
                    Name = model.Name,
                    Description = model.Description,
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value
                };

                conference = _repository.Save(conference);

                return RedirectToAction("Details", new {conference.Id });
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

        [HttpPost]
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

                return RedirectToAction("Details", new {model.Id});
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            try
            {
                var conference = _repository.FindById(id);

                var model = new ConferenceViewModel
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
    }
}