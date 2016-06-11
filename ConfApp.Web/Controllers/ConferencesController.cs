﻿using System;

namespace ConfApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Domain.Data;
    using Domain.Models;
    using Models.Conferences;

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

            _repository.Save(conference);

            return RedirectToAction("");
        }

        public ActionResult Edit(Guid id)
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
    }
}