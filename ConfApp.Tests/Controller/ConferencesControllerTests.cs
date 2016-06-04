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
    }
}