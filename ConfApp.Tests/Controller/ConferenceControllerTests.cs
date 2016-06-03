using ConfApp.Web.Controllers;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests.Controller
{
    public class ConferenceControllerTests
    {
        [Fact]
        public void Index_ShouldReturnAListOfConferences()
        {
            var subject = new ConferenceController();
            var result = subject.Index();

            result.Should().NotBeNull();
        }
    }
}