namespace ConfApp.Domain.Data
{
    using System;
    using System.Linq;
    using Models;

    public interface IConferenceRepository
    {
        IQueryable<Conference> Query();
        Conference FindById(Guid id);
        Conference Save(Conference conference);
        
    }
}