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
        public void Create_ShouldReturnAViewIfTheModelIsInvalid()
        {
            // ARRANGE
            var model = new CreateConference();
            var repository = A.Fake<IConferenceRepository>();
            var subject = new ConferencesController(repository);
            subject.ModelState.AddModelError("aProp", "Something is wrong");

            // ACT
            var result = subject.Create(model).GetAwaiter().GetResult();

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

            var repository = A.Fake<IConferenceRepository>();
            var subject = new ConferencesController(repository);

            // ACT
            var result = subject.Create(model).GetAwaiter().GetResult();

            // ASSERT
            A.CallTo(() => repository.Save(A<Conference>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToRouteResult>();
        }

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
            result.As<ViewResult>().Model.Should().BeOfType(typeof(ConferenceList));

            var model = result.As<ViewResult>().Model as ConferenceList;
            model.Items.Should().NotBeEmpty();
            model.Items.Any(x => x.Id == expectedConference.Id).Should().BeTrue();
        }
    }
}