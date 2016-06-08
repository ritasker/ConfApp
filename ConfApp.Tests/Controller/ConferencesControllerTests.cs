using System;
using System.Linq;
using System.Web.Mvc;
using ConfApp.Tests.Stubs;
using ConfApp.Web.Controllers;
using ConfApp.Web.Data;
using ConfApp.Web.Data.Models;
using ConfApp.Web.Models.Conferences;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests.Controller
{
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

            var mockData = new FakeDbSet<Conference>
            {
                new Conference
                {
                    Id = expectedConference.Id,
                    Name = expectedConference.Name
                }
            };

            var context = A.Fake<IContext>();
            A.CallTo(() => context.Conferences).Returns(mockData);

            var subject = new ConferencesController(context);

            // ACT
            var result = subject.Index();

            // ASSERT
            result.Should().NotBeNull();
            result.As<ViewResult>().Model.Should().BeOfType(typeof(ConferenceList));

            var model = result.As<ViewResult>().Model as ConferenceList;
            model.Items.Should().NotBeEmpty();
            model.Items.Any(x => x.Id == expectedConference.Id).Should().BeTrue();
        }

        [Fact]
        public void Create_ShouldSaveTheConference()
        {
            // ARRANGE
            var model = new CreateConference
            {
                Name = Faker.Lorem.GetFirstWord(),
                Description = Faker.Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var context = A.Fake<IContext>();
            var subject = new ConferencesController(context);

            // ACT
            var result = subject.Create(model).GetAwaiter().GetResult();

            // ASSERT
            A.CallTo(() => context.SaveChangesAsync()).MustHaveHappened(Repeated.Exactly.Once);
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToRouteResult>();
        }

        [Fact]
        public void Create_ShouldReturnAViewIfTheModelIsInvalid()
        {
            // ARRANGE
            var model = new CreateConference();
            var context = A.Fake<IContext>();
            var subject = new ConferencesController(context);
            subject.ModelState.AddModelError("aProp", "Something is wrong");

            // ACT
            var result = subject.Create(model).GetAwaiter().GetResult();

            // ASSERT
            A.CallTo(() => context.SaveChangesAsync()).MustHaveHappened(Repeated.Never);
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }
    }
}