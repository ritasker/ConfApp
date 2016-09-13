using System;
using System.Linq;
using ConfApp.Data.Repositories;
using ConfApp.Domain;
using ConfApp.Domain.Exceptions;
using ConfApp.Domain.Models;
using ConfApp.Tests.Stubs;
using FakeItEasy;
using Faker;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests.Repositories
{
    public class ConferenceRepositoryTests
    {
        [Fact]
        public void SaveShouldSaveAndReturnANewConference()
        {
            var conference = new Conference
            {
                Name = string.Join(" ", Lorem.Words(3)),
                Description = Lorem.Sentence(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.Conferences).Returns(new FakeDbSet<ConferenceDetails>());
            
            var subject = new ConferenceRepository(context);

            var result = subject.Save(conference);

            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            A.CallTo(() => context.SaveChangesAsync()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void FindAllShouldReturnAListOfConferenceSummaries()
        {
            var conference = new FakeDbSet<ConferenceSummary>
            {
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                },
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                },
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                }
            };

            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.ConferenceSummaries).Returns(conference);

            var subject = new ConferenceRepository(context);

            var result = subject.FindAll(1, 0);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
        }

        [Fact]
        public void FindAllShouldSkipAndTakeRecords()
        {
            var conference = new FakeDbSet<ConferenceSummary>
            {
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                },
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                },
                new ConferenceSummary
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3))
                }
            };

            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.ConferenceSummaries).Returns(conference);

            var subject = new ConferenceRepository(context);

            var result = subject.FindAll(1, 1);

            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result.First().Id.Should().Be(conference.OrderBy(x => x.Name).ElementAt(1).Id);
        }

        [Fact]
        public void FindAllShouldReturnsAnEmptyList()
        {
            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.ConferenceSummaries).Returns(new FakeDbSet<ConferenceSummary>());

            var subject = new ConferenceRepository(context);

            var result = subject.FindAll(1, 0);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void FindByIdShouldReturnAConference()
        {
            var id = Guid.NewGuid();

            var set = new FakeDbSet<ConferenceDetails>
            {
                new ConferenceDetails
                {
                    Id = id,
                    Name = string.Join(" ", Lorem.Words(3)),
                    Description = Lorem.Sentence(),
                    StartDate = DateTime.UtcNow.AddDays(3),
                    EndDate = DateTime.UtcNow.AddDays(6)
                }
            };

            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.Conferences).Returns(set);
            
            var subject = new ConferenceRepository(context);

            var result = subject.FindById(id);

            A.CallTo(() => context.Conferences).MustHaveHappened(Repeated.Exactly.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<ConferenceDetails>();
            result.Id.Should().Be(id);
        }

        [Fact]
        public void FindByIdShouldThrowAnException()
        {
            var set = new FakeDbSet<ConferenceDetails>
            {
                new ConferenceDetails
                {
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ", Lorem.Words(3)),
                    Description = Lorem.Sentence(),
                    StartDate = DateTime.UtcNow.AddDays(3),
                    EndDate = DateTime.UtcNow.AddDays(6)
                }
            };

            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.Conferences).Returns(set);

            var subject = new ConferenceRepository(context);

            Assert.Throws<EntityNotFoundException>(() => subject.FindById(Guid.NewGuid()));
        }
    }
}