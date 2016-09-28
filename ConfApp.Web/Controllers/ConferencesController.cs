using System;
using System.Web.Mvc;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Data;
using ConfApp.Domain.Exceptions;
using ConfApp.Domain.Infrastructure;
using ConfApp.Domain.Models;
using ConfApp.Web.Models.Conferences;

namespace ConfApp.Web.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly ICommandRouter _commandRouter;
        private readonly IConferenceRepository _repository;

        public ConferencesController(IConferenceRepository repository, ICommandRouter commandRouter)
        {
            _repository = repository;
            _commandRouter = commandRouter;
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
            var id = _commandRouter.Issue(command);
            return RedirectToAction("Details", new {id});
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
            var conference = _repository.FindById(model.Id);

            conference.Name = model.Name;
            conference.Description = model.Description;
            conference.StartDate = model.StartDate.Value;
            conference.EndDate = model.EndDate.Value;

            _repository.Save(new Conference(conference));

            return RedirectToAction("Details", new {model.Id});
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