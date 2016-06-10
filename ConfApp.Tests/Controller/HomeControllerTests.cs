namespace ConfApp.Tests.Controller
{
    using FluentAssertions;
    using Web.Controllers;
    using Xunit;

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