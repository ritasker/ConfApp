namespace ConfApp.Tests.Repositories
{
    using Data.Repositories;
    using Xunit;

    public class ConferenceRepositoryTests
    {
        [Fact]
        public void QueryShouldReturnAQueryable()
        {
            var subject = new ConferenceRepository();
        }
    }
}