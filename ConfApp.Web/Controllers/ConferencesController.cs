using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Infrastructure;

namespace ConfApp.Web.Controllers
{
    using System.Web.Mvc;
    using Domain.Data;
    using Domain.Models;
    using Models.Conferences;
    using System;
    using Domain.Exceptions;

    public class ConferencesController : Controller
    {
        private readonly IConferenceRepository _repository;
        private readonly IMediator _mediator;

        public ConferencesController(IConferenceRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
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
        public ActionResult Create(CreateConference command)
        {
            if (ModelState.IsValid)
            {
                Guid id = _mediator.Issue(command);
                return RedirectToAction("Details", new {id });
            }

            return View(command);
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

                _repository.Save(new Conference(conference));

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
                return View(conference);
            }
            catch (EntityNotFoundException ex)
            {
                return new HttpNotFoundResult(ex.Message);
            }
        }
    }
}