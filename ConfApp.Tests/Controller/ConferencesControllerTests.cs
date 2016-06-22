using ConfApp.Domain.Exceptions;

namespace ConfApp.Tests.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Domain.Data;
    using Domain.Models;
    using FakeItEasy;
    using Faker;
    using FluentAssertions;
    using Web.Controllers;
    using Web.Models.Conferences;
    using Xunit;

    public class ConferencesControllerTests
    {
        [Fact]
        public void Index_ShouldReturnAListOfConferences()
        {
            // ARRANGE
            var expectedConference = new ConferenceSummary
            {
                Id = Guid.NewGuid(),
                Name = "DevConf"
            };

            var mockData = new List<Conference>
            {
                new Conference
                {
                    Id = expectedConference.Id,
                    Name = expectedConference.Name
                }
            };

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.Query()).Returns(mockData.AsQueryable());

            var subject = new ConferencesController(repository);

            // ACT
            var result = subject.Index();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType(typeof(ConferenceList));

            var model = result.As<ViewResult>().Model as ConferenceList;
            model.Items.Should().NotBeEmpty();
            model.Items.Any(x => x.Id == expectedConference.Id).Should().BeTrue();
        }

        [Fact]
        public void Create_ShouldReturnAView()
        {
            // ARRANGE
            var subject = new ConferencesController(null);

            // ACT
            var result = subject.Create();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Create_ShouldReturnAViewIfTheModelIsInvalid()
        {
            // ARRANGE
            var model = new CreateConference();
            var repository = A.Fake<IConferenceRepository>();
            var subject = new ConferencesController(repository);
            subject.ModelState.AddModelError("aProp", "Something is wrong");

            // ACT
            var result = subject.Create(model);

            // ASSERT
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).MustHaveHappened(Repeated.Never);
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Create_ShouldSaveTheConference()
        {
            // ARRANGE
            var model = new CreateConference
            {
                Name = Lorem.GetFirstWord(),
                Description = Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var conference = new Conference
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate.Value,
                EndDate = model.EndDate.Value
            };

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).Returns(conference);

            var subject = new ConferencesController(repository);

            // ACT
            var result = subject.Create(model);

            // ASSERT
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToRouteResult>();
            result.As<RedirectToRouteResult>().RouteValues["id"].Should().Be(conference.Id);
        }

        [Fact]
        public void Edit_ShouldReturnAView()
        {
            var conferenceId = Guid.NewGuid();

            Conference conference = new Conference
            {
                Id = conferenceId,
                Name = Lorem.GetFirstWord(),
                Description = Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.FindById(conferenceId)).Returns(conference);

            var subject = new ConferencesController(repository);

            var result = subject.Edit(conferenceId);

            A.CallTo(() => repository.FindById(conferenceId)).MustHaveHappened(Repeated.Exactly.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();

            var viewResult = result as ViewResult;
            viewResult.Model.Should().BeOfType(typeof(EditConference));

            var model = viewResult.Model as EditConference;
            model.Id.Should().Be(conferenceId);
        }

        [Fact]
        public void Edit_ShouldReturnA404()
        {
            var conferenceId = Guid.NewGuid();
            var entityNotFoundException = new EntityNotFoundException(nameof(Conference), conferenceId.ToString());

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.FindById(conferenceId)).Throws(entityNotFoundException);

            var subject = new ConferencesController(repository);

            var result = subject.Edit(conferenceId);

            A.CallTo(() => repository.FindById(conferenceId)).MustHaveHappened(Repeated.Exactly.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<HttpNotFoundResult>();

            var notFoundResult = result as HttpNotFoundResult;
            notFoundResult.StatusDescription.Should().Be(entityNotFoundException.Message);
        }

        [Fact]
        public void Edit_ShouldReturnAViewIfTheModelIsInvalid()
        {
            // ARRANGE
            var model = new EditConference();
            var repository = A.Fake<IConferenceRepository>();
            var subject = new ConferencesController(repository);
            subject.ModelState.AddModelError("aProp", "Something is wrong");

            // ACT
            var result = subject.Edit(model);

            // ASSERT
            A.CallTo(() => repository.FindById(model.Id)).MustHaveHappened(Repeated.Never);
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).MustHaveHappened(Repeated.Never);
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Edit_ShouldSaveTheConference()
        {
            // ARRANGE
            var model = new EditConference
            {
                Id = Guid.NewGuid(),
                Name = Lorem.GetFirstWord(),
                Description = Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var repository = A.Fake<IConferenceRepository>();
            var subject = new ConferencesController(repository);

            // ACT
            var result = subject.Edit(model);

            // ASSERT
            A.CallTo(() => repository.FindById(model.Id)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToRouteResult>();

            result.As<RedirectToRouteResult>().RouteValues["id"].Should().Be(model.Id);
        }

        [Fact]
        public void Details_ShouldReturnAConference()
        {
            var id = Guid.NewGuid();
            var conference = new Conference
            {
                Id = id,
                Name = Lorem.GetFirstWord(),
                Description = Lorem.Sentence(5),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.FindById(id)).Returns(conference);
            var subject = new ConferencesController(repository);

            var result = subject.Details(id);

            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();

            result.As<ViewResult>().Model.Should().BeOfType(typeof(ConferenceViewModel));
            result.As<ViewResult>().Model.As<ConferenceViewModel>().Id.Should().Be(id);

            A.CallTo(() => repository.FindById(id)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void Details_ShouldReturnA404()
        {
            var id = Guid.NewGuid();
            var entityNotFoundException = new EntityNotFoundException(nameof(Conference), id.ToString());

            var repository = A.Fake<IConferenceRepository>();
            A.CallTo(() => repository.FindById(id)).Throws(entityNotFoundException);

            var subject = new ConferencesController(repository);

            var result = subject.Details(id);

            A.CallTo(() => repository.FindById(id)).MustHaveHappened(Repeated.Exactly.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<HttpNotFoundResult>();

            var notFoundResult = result as HttpNotFoundResult;
            notFoundResult.StatusDescription.Should().Be(entityNotFoundException.Message);
        }
    }
}