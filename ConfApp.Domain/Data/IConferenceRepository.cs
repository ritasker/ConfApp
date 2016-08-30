namespace ConfApp.Domain.Data
{
    using System;
    using Models;
    using System.Collections.Generic;

    public interface IConferenceRepository
    {
        List<ConferenceSummary> FindAll(int top, int skip);
        ConferenceDetails FindById(Guid id);
        ConferenceDetails Save(ConferenceDetails conference);
    }
}