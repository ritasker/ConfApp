using System;
using ConfApp.Domain;
using ConfApp.Domain.Conferences.Commands;
using ConfApp.Domain.Conferences.Handlers;
using ConfApp.Domain.Models;
using ConfApp.Tests.Stubs;
using FakeItEasy;
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
            var context = A.Fake<IReadContext>();
            A.CallTo(() => context.Conferences).Returns(new FakeDbSet<ConferenceDetails>());
            var subject = new CreateConfereceHandler();

            // ACT
            Guid result = subject.Handle(new CreateConference());

            // ASSERT
            result.Should().NotBeEmpty();
        }
    }
}