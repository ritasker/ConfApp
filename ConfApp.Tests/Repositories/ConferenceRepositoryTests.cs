using System;
using System.Linq;
using ConfApp.Domain;
using ConfApp.Domain.Models;
using ConfApp.Tests.Stubs;
using FakeItEasy;
using FluentAssertions;

namespace ConfApp.Tests.Repositories
{
    using Data.Repositories;
    using Xunit;

    public class ConferenceRepositoryTests
    {
        [Fact]
        public void QueryShouldReturnAQueryable()
        {
            var conference = new Conference
            {
                Id = Guid.NewGuid(),
                Name = string.Join(" ", Faker.Lorem.Words(3)),
                Description = Faker.Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var set = new FakeDbSet<Conference>
            {
                conference
            };

            var context = A.Fake<IContext>();
            A.CallTo(() => context.Conferences).Returns(set);

            var subject = new ConferenceRepository(context);

            var result = subject.Query();

            result.Should().NotBeNull();
            result.First().Id.Should().Be(conference.Id);
        }

        [Fact]
        public void SaveShouldSaveAndReturnANewConference()
        {
            var conference = new Conference
            {
                Name = string.Join(" ", Faker.Lorem.Words(3)),
                Description = Faker.Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var context = A.Fake<IContext>();
            A.CallTo(() => context.Conferences).Returns(new FakeDbSet<Conference>());
            
            var subject = new ConferenceRepository(context);

            var result = subject.Save(conference);

            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            A.CallTo(() => context.SaveChangesAsync()).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}