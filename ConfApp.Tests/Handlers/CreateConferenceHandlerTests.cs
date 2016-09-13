using System;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Conferences.Handlers;
using FluentAssertions;
using Xunit;

namespace ConfApp.Tests.Handlers
{
    public class CreateConferenceHandlerTests
    {
        [Fact]
        public void ShouldReturnTheIdOfTheNewConference()
        {
            // ARRANGE
            var createConference = new CreateConference
            {
                Name = string.Join(" ", Faker.Lorem.Words(3)),
                Description = string.Join(" ", Faker.Lorem.Sentence()),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(6)
            };

            var subject = new CreateConfereceHandler();

            // ACT
            Guid result = subject.Handle(createConference);

            // ASSERT
            result.Should().NotBeEmpty();
        }
    }
}