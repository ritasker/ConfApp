using ConfApp.Web.Controllers;
using Xunit;
using FluentAssertions;

namespace ConfApp.Tests.Controller
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ShouldReturnAView()
        {
            var subject = new HomeController();

            var result = subject.Index();

            result.Should().NotBeNull();
        }
    }
}
