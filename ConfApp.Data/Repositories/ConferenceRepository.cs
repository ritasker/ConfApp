using ConfApp.Domain;

namespace ConfApp.Data.Repositories
{
    using System.Linq;
    using Domain.Data;
    using Domain.Models;

    public class ConferenceRepository : IConferenceRepository
    {
        private readonly IContext _context;

        public ConferenceRepository(IContext context)
        {
            _context = context;
        }

        public IQueryable<Conference> Query()
        {
            return _context.Conferences;
        }

        public Conference Save(Conference conference)
        {
            _context.Conferences.Add(conference);
            _context.SaveChangesAsync().Wait();

            return conference;
        }
    }
}