using System;
using ConfApp.Domain;
using ConfApp.Domain.Exceptions;

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

        public Conference FindById(Guid id)
        {
            try
            {
                return _context
                .Conferences
                .First(x => x.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new EntityNotFoundException(nameof(Conference), id.ToString(), ex);
            }
        }

        public Conference Save(Conference conference)
        {
            conference.Id = Guid.NewGuid();
            _context.Conferences.Add(conference);
            _context.SaveChangesAsync().Wait();

            return conference;
        }
    }
}