using System.Collections.Generic;

namespace ConfApp.Domain.Data
{
    using System;
    using Models;

    public interface IConferenceRepository
    {
        List<ConferenceSummary> FindAll(int top, int skip);
        ConferenceDetails FindById(Guid id);
        ConferenceDetails Save(ConferenceDetails conference);
    }
}