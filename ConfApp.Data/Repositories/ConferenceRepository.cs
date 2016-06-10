namespace ConfApp.Data.Repositories
{
    using System.Linq;
    using Domain.Data;
    using Domain.Models;

    public class ConferenceRepository : IConferenceRepository
    {
        public IQueryable<Conference> Query()
        {
            throw new System.NotImplementedException();
        }

        public Conference Save(Conference conference)
        {
            throw new System.NotImplementedException();
        }
    }
}